using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Computer.Infrastructure.Core;
using Computer.Model.Models;
using Computer.Models.Computer;
using Computer.Service;
using Computer.Infrastructure.Extensions;

namespace Computer.Controllers
{
    [RoutePrefix("api/fakedata")]
    [Authorize]
    public class FakeDataController : ApiControllerBase
    {
        private readonly IComputerService _computerService;
        private readonly IComputerTypeService _computerTypeService;
        private readonly IDeparmentTypeService _deparmentTypeService;
        private readonly IProducerTypeService _producerTypeService;

        public FakeDataController(
            IErrorService errorService, 
            IComputerService computerService, 
            IComputerTypeService computerTypeService,
            IDeparmentTypeService deparmentTypeService, 
            IProducerTypeService producerTypeService) :
            base(errorService)
        {
            this._computerService = computerService;
            _computerTypeService = computerTypeService;
            _deparmentTypeService = deparmentTypeService;
            _producerTypeService = producerTypeService;
        }

        [HttpGet]
        [Route("MassGenerateData")]
        public void MassGenerateData()
        {
            MassCreateComputerTypes();

            MassCreateDeparmentTypes();

            MassCreateProducerTypes();

            //MassCreateComputers();
        }

        [HttpGet]
        [Route("MassCreateComputerTypes")]
        public void MassCreateComputerTypes()
        {
            var fakeComputerTypes = new List<ComputerTypeViewModel>();
            for (int i = 1; i < 6; i++)
            {
                var computerType = new ComputerTypeViewModel()
                {
                    ComputerTypeCode = $"CTCODE{i}",
                    ComputerTypeName = $"ComputerTypeName {i}",
                    ComputerTypeDescription = $"ComputerTypeDesription {i}",
                    Status = true
                };
                fakeComputerTypes.Add(computerType);
            }

            foreach (var computerTypeViewModel in fakeComputerTypes)
            {
                ComputerType newComputerType = new ComputerType();
                newComputerType.UpdateComputerType(computerTypeViewModel);

                _computerTypeService.Add(newComputerType);
                _computerTypeService.Save();
            }
        }

        [HttpGet]
        [Route("MassCreateDeparmentTypes")]
        public void MassCreateDeparmentTypes()
        {
            var fakeDeparmentTypes = new List<DeparmentTypeViewModel>();
            for (int i = 1; i < 6; i++)
            {
                var deparmentType = new DeparmentTypeViewModel()
                {
                    DeparmentTypeCode = $"DTCODE{i}",
                    DeparmentTypeName = $"DeparmentTypeName {i}",
                    DeparmentTypeDescription = $"DeparmentTypeDesription {i}",
                    Status = true
                };
                fakeDeparmentTypes.Add(deparmentType);
            }

            foreach (var deparmentTypeViewModel in fakeDeparmentTypes)
            {
                DeparmentType newDeparmentType = new DeparmentType();
                newDeparmentType.UpdateDeparmentType(deparmentTypeViewModel);

                _deparmentTypeService.Add(newDeparmentType);
                _deparmentTypeService.Save();
            }
        }

        [HttpGet]
        [Route("MassCreateProducerTypes")]
        public void MassCreateProducerTypes()
        {
            var fakeProducerTypes = new List<ProducerTypeViewModel>();
            for (int i = 1; i < 6; i++)
            {
                var producerType = new ProducerTypeViewModel()
                {
                    ProducerTypeCode = $"PTCODE{i}",
                    ProducerTypeName = $"ProducerTypeName {i}",
                    ProducerTypeDescription = $"ProducerTypeDesription {i}",
                    Status = true
                };
                fakeProducerTypes.Add(producerType);
            }

            foreach (var producerTypeViewModel in fakeProducerTypes)
            {
                ProducerType newProducerType = new ProducerType();
                newProducerType.UpdateProducerType(producerTypeViewModel);

                _producerTypeService.Add(newProducerType);
                _producerTypeService.Save();
            }
        }

        [HttpGet]
        [Route("MassCreateComputers")]
        public void MassCreateComputers()
        {
            var fakeComputers = new List<ComputerViewModel>();
            for (int i = 1; i < 6; i++)
            {
                var computer = new ComputerViewModel()
                {
                    ComputerCode = $"CCODE{i}",
                    ComputerName = $"ComputerName {i}",
                    ComputerDescription = $"ComputerDesription {i}",
                    ComputerTypeId = i,
                    DeparmentTypeId = i,
                    ProducerTypeId = i,
                    Status = true
                };
                fakeComputers.Add(computer);
            }
            
            foreach (var computerViewModel in fakeComputers)
            {
                Model.Models.Computer newComputer = new Model.Models.Computer();
                newComputer.UpdateComputer(computerViewModel);

                _computerService.Add(newComputer);
                _computerService.Save();
            }
        }
    }
}
