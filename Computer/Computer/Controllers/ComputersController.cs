﻿using AutoMapper;
using System.Collections.Generic;
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
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow;

                var model = _computerService.GetAllPagingWithFilter(page, pageSize, out totalRow, filter);
                var modelVm = Mapper.Map<List<Model.Models.Computer>, List<ComputerViewModel>>(model);

                var pagedSet = new PaginationSet<ComputerViewModel>()
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
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listComputer = _computerService.GetAll();

                var listComputerVm = Mapper.Map<List<ComputerViewModel>>(listComputer);

                var response = request.CreateResponse(HttpStatusCode.OK, listComputerVm);

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

            var computer = _computerService.GetById(id);
            if (computer == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }

            var orderVm = Mapper.Map<Model.Models.Computer, ComputerViewModel>(computer);

            return request.CreateResponse(HttpStatusCode.OK, orderVm);
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
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Model.Models.Computer newComputer = new Model.Models.Computer();
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
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
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
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
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