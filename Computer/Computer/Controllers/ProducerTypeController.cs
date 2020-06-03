using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Computer.Common;
using Computer.Infrastructure.Core;
using Computer.Infrastructure.Extensions;
using Computer.Model.Models;
using Computer.Models.Computer;
using Computer.Service;

namespace Computer.Controllers
{
    [Authorize]
    [RoutePrefix("api/producerType")]
    public class ProducerTypeController : ApiControllerBase
    {
        private readonly IProducerTypeService _producerTypeService;

        public ProducerTypeController(IErrorService errorService, IProducerTypeService producerTypeService) : base(errorService)
        {
            _producerTypeService = producerTypeService;
        }

        [HttpGet]
        [Route("getlistpaging")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int pageIndex, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow;

                var model = _producerTypeService.GetAllPagingWithFilter(pageIndex, pageSize, out totalRow, filter);
                var modelVm = Mapper.Map<List<ProducerType>, List<ProducerTypeViewModel>>(model);

                var pagedSet = new PaginationSet<ProducerTypeViewModel>()
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
        public HttpResponseMessage GetProducerTypeSelecList(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listProducerType = _producerTypeService.GetAll();

                var listProducerTypeVm = Mapper.Map<List<ProducerTypeSelectListViewModel>>(listProducerType);

                var response = request.CreateResponse(HttpStatusCode.OK, listProducerTypeVm);

                return response;
            });
        }

        [HttpGet]
        [Route("detail/{id}")]
        public HttpResponseMessage GetDetailById(HttpRequestMessage request, int id)
        {
            if (!_producerTypeService.CheckExistedId(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id Not Found!");
            }

            var producerType = _producerTypeService.GetById(id);
            if (producerType == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }

            var producerTypeViewModel = Mapper.Map<ProducerType, ProducerTypeViewModel>(producerType);

            return request.CreateResponse(HttpStatusCode.OK, producerTypeViewModel);
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, ProducerTypeViewModel producerTypeVm)
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
                    var newProducerType = new ProducerType();
                    newProducerType.UpdateProducerType(producerTypeVm);

                    var producerType = _producerTypeService.Add(newProducerType);
                    _producerTypeService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, producerType);
                }
                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, ProducerTypeViewModel producerTypeVm)
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
                    var producerTypeDb = _producerTypeService.GetById(producerTypeVm.ProducerTypeId);
                    producerTypeDb.UpdateProducerType(producerTypeVm);
                    _producerTypeService.Update(producerTypeDb);
                    _producerTypeService.Save();

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
                if (!_producerTypeService.CheckExistedId(id))
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id Not Found!");
                }
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage);
                }
                else
                {
                    try
                    {
                        _producerTypeService.Delete(id);
                        _producerTypeService.Save();

                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                    catch (Exception)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, CommonConstants.CannotDeleteProducerType);
                    }                   
                }

                return response;
            });
        }
    }
}
