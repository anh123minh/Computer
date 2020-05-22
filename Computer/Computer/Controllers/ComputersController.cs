using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Computer.Infrastructure.Core;
using Computer.Service;
using Computer.Infrastructure.Extensions;
using Computer.Models;

namespace Computer.Controllers
{
    [RoutePrefix("api/computer")]
    [Authorize]
    public class ComputerController : ApiControllerBase
    {
        private IComputerService _computerService;

        public ComputerController(IErrorService errorService, IComputerService computerService) :
            base(errorService)
        {
            this._computerService = computerService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listComputer = _computerService.GetAll();

                var listComputerVm = Mapper.Map<List<ComputerViewModel>>(listComputer);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listComputerVm);

                return response;
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, ComputerViewModel computerVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
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

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, ComputerViewModel computerVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
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

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
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