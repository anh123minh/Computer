using System.Net;
using System.Net.Http;
using System.Web.Http;
using Computer.Service;
using Computer.Infrastructure.Core;

namespace Computer.Controllers
{
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            _statisticService = statisticService;
        }
        
        [Route("getComputerStatisticByComputerType")]
        [HttpGet]
        public HttpResponseMessage GetComputerStatisticByComputerType(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetComputerStatisticByComputerType();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getComputerStatisticByProducerType")]
        [HttpGet]
        public HttpResponseMessage GetComputerStatisticByProducerType(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetComputerStatisticByProducerType();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getComputerStatisticByUsingUnit")]
        [HttpGet]
        public HttpResponseMessage GetComputerStatisticByUsingUnit(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetComputerStatisticByUsingUnit();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}