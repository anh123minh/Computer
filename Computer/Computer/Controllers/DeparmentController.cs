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
    [RoutePrefix("api/deparmentType")]
    public class DeparmentController : ApiControllerBase
    {
        private readonly IDeparmentTypeService _deparmentTypeService;

        public DeparmentController(IErrorService errorService, IDeparmentTypeService deparmentTypeService) : base(errorService)
        {
            _deparmentTypeService = deparmentTypeService;
        }

        [HttpGet]
        [Route("getlistpaging")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow;

                var model = _deparmentTypeService.GetAllPagingWithFilter(page, pageSize, out totalRow, filter);
                var modelVm = Mapper.Map<List<DeparmentType>, List<DeparmentTypeViewModel>>(model);

                var pagedSet = new PaginationSet<DeparmentTypeViewModel>()
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

            var deparmentType = _deparmentTypeService.GetById(id);
            if (deparmentType == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }

            var orderVm = Mapper.Map<DeparmentType, DeparmentTypeViewModel>(deparmentType);

            return request.CreateResponse(HttpStatusCode.OK, orderVm);
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, DeparmentTypeViewModel deparmentTypeVm)
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
                    var newDeparmentType = new DeparmentType();
                    newDeparmentType.UpdateDeparmentType(deparmentTypeVm);

                    var deparmentType = _deparmentTypeService.Add(newDeparmentType);
                    _deparmentTypeService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, deparmentType);
                }
                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, DeparmentTypeViewModel deparmentTypeVm)
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
                    var deparmentTypeDb = _deparmentTypeService.GetById(deparmentTypeVm.DeparmentTypeId);
                    deparmentTypeDb.UpdateDeparmentType(deparmentTypeVm);
                    _deparmentTypeService.Update(deparmentTypeDb);
                    _deparmentTypeService.Save();

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
                    _deparmentTypeService.Delete(id);
                    _deparmentTypeService.Save();
                    
                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }
    }
}
