using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Computer.Infrastructure.Core;
using Computer.Infrastructure.Extensions;
using Computer.Model.Models;
using Computer.Models.Computer;
using Computer.Service;

namespace Computer.Controllers
{
    [Authorize]
    [RoutePrefix("api/computerType")]
    public class ComputerTypeController : ApiControllerBase
    {
        private readonly IComputerTypeService _computerTypeService;
        
        public ComputerTypeController(IErrorService errorService, IComputerTypeService computerTypeService) : base(errorService)
        {
            _computerTypeService = computerTypeService;
        }

        [HttpGet]
        [Route("getlistpaging")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow;

                var model = _computerTypeService.GetAllPagingWithFilter(page, pageSize, out totalRow, filter);
                var modelVm = Mapper.Map<List<ComputerType>, List<ComputerTypeViewModel>>(model);

                var pagedSet = new PaginationSet<ComputerTypeViewModel>()
                {
                    PageIndex = page,
                    PageSize = pageSize,
                    TotalRows = totalRow,
                    Items = modelVm,
                };

                var response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        
        [HttpGet]
        [Route("detail/{id}")]
        public HttpResponseMessage GetDetailById(HttpRequestMessage request, int id)
        {
            if (id == 0)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
            }

            var computerType = _computerTypeService.GetById(id);
            if (computerType == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }

            var orderVm = Mapper.Map<ComputerType, ComputerTypeViewModel>(computerType);

            return request.CreateResponse(HttpStatusCode.OK, orderVm);
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, ComputerTypeViewModel computerTypeVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newComputerType = new ComputerType();
                    newComputerType.UpdateComputerType(computerTypeVm);

                    var computerType = _computerTypeService.Add(newComputerType);
                    _computerTypeService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, computerType);
                }
                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, ComputerTypeViewModel computerTypeVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var computerTypeDb = _computerTypeService.GetById(computerTypeVm.ComputerTypeId);
                    computerTypeDb.UpdateComputerType(computerTypeVm);
                    _computerTypeService.Update(computerTypeDb);
                    _computerTypeService.Save();

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
                HttpResponseMessage response;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _computerTypeService.Delete(id);
                    _computerTypeService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }
    }
}
