using System;
using System.Collections.Generic;
using System.Linq;
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
    [RoutePrefix("api/computerUsingHistory")]
    public class ComputerUsingHistoryController : ApiControllerBase
    {
        private readonly IComputerUsingHistoryService _computerUsingHistoryService;

        public ComputerUsingHistoryController(IErrorService errorService, IComputerUsingHistoryService computerUsingHistory) : base(errorService)
        {
            _computerUsingHistoryService = computerUsingHistory;
        }

        [HttpGet]
        [Route("getlistpaging")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int pageIndex, int pageSize, int? deparmentTypeId, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow;

                var model = _computerUsingHistoryService.GetAllPagingWithFilterDeparmentTypeId(pageIndex, pageSize, out totalRow, deparmentTypeId, filter);
                var modelVm = Mapper.Map<List<ComputerUsingHistory>, List<ComputerUsingHistoryDetailViewModel>>(model);

                var pagedSet = new PaginationSet<ComputerUsingHistoryDetailViewModel>()
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
        [Route("detail/{id}")]
        public HttpResponseMessage GetDetailById(HttpRequestMessage request, int id)
        {
            if (!_computerUsingHistoryService.CheckExistedId(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id Not Found!");
            }

            var computerUsingHistory = _computerUsingHistoryService.GetById(id);
            if (computerUsingHistory == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }

            var computerUsingHistoryDetailViewModel = Mapper.Map<ComputerUsingHistory, ComputerUsingHistoryDetailViewModel>(computerUsingHistory);

            return request.CreateResponse(HttpStatusCode.OK, computerUsingHistoryDetailViewModel);
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, ComputerUsingHistoryViewModel computerTypeVm)
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
                    var newComputerUsingHistory = new ComputerUsingHistory();
                    newComputerUsingHistory.UpdateComputerUsingHistory(computerTypeVm);

                    var computerUsingHistory = _computerUsingHistoryService.Add(newComputerUsingHistory);
                    _computerUsingHistoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, computerUsingHistory);
                }
                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, ComputerUsingHistoryViewModel computerTypeVm)
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
                    var computerTypeDb = _computerUsingHistoryService.GetById(computerTypeVm.ComputerUsingHistoryId);
                    computerTypeDb.UpdateComputerUsingHistory(computerTypeVm);
                    _computerUsingHistoryService.Update(computerTypeDb);
                    _computerUsingHistoryService.Save();

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
                if (!_computerUsingHistoryService.CheckExistedId(id))
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
                        _computerUsingHistoryService.Delete(id);
                        _computerUsingHistoryService.Save();

                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                    catch (Exception e)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, e.InnerException?.Message);
                    }                   
                }

                return response;
            });
        }
    }
}
