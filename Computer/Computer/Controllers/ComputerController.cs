using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Computer.Infrastructure.Core;
using Computer.Service;
using Computer.Infrastructure.Extensions;
using Computer.Models.Computer;

namespace Computer.Controllers
{
    [Authorize]
    [RoutePrefix("api/computer")]    
    public class ComputerController : ApiControllerBase
    {
        private readonly IComputerService _computerService;

        public ComputerController(IErrorService errorService, IComputerService computerService) :
            base(errorService)
        {
            _computerService = computerService;
        }

        [HttpGet]
        [Route("getlistpaging")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int pageIndex, int pageSize,
            int? computerTypeId, int? deparmentTypeId, int? producerTypeId, string filter = "")
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow;

                var model = _computerService.GetAllPagingWithMultiFilters(pageIndex, pageSize, out totalRow, computerTypeId, deparmentTypeId, producerTypeId, filter);
                var modelVm = Mapper.Map<List<Model.Models.Computer>, List<ComputerDetailViewModel>>(model);

                var pagedSet = new PaginationSet<ComputerDetailViewModel>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalRows = totalRow,
                    Items = modelVm,
                };

                var response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [HttpGet]
        [Route("selectlist")]
        public HttpResponseMessage GetComputerSelecList(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listComputer = _computerService.GetAll();

                var listComputerVm = Mapper.Map<List<ComputerSelectListViewModel>>(listComputer);

                var response = request.CreateResponse(HttpStatusCode.OK, listComputerVm);

                return response;
            });
        }

        [HttpGet]
        [Route("detail/{id}")]
        public HttpResponseMessage GetDetailById(HttpRequestMessage request, int id)
        {
            if (!_computerService.CheckExistedId(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id Not Found!");
            }

            var computer = _computerService.GetSingleDeepById(id);
            if (computer == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }

            var computerViewModel = Mapper.Map<Model.Models.Computer, ComputerDetailViewModel>(computer);

            return request.CreateResponse(HttpStatusCode.OK, computerViewModel);
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, ComputerViewModel computerVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage);
                }
                else
                {
                    var newComputer = new Model.Models.Computer();
                    newComputer.UpdateComputer(computerVm);

                    var computer = _computerService.Add(newComputer);
                    _computerService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, computer);
                }
                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, ComputerViewModel computerVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage);
                }
                else
                {
                    var computerDb = _computerService.GetById(computerVm.ComputerId);
                    computerDb.UpdateComputer(computerVm);
                    _computerService.Update(computerDb);
                    _computerService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!_computerService.CheckExistedId(id))
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id Not Found!");
                }
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage);
                }
                else
                {
                    _computerService.Delete(id);
                    _computerService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
    }
}