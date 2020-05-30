using System.Collections.Generic;
using Computer.Data.Repositories;

namespace Computer.Service
{
    public interface IStatisticService
    {

        IEnumerable<ComputerStatisticByComputerType> GetComputerStatisticByComputerType();

        IEnumerable<ComputerStatisticByProducerType> GetComputerStatisticByProducerType();

        IEnumerable<ComputerStatisticByUsingUnit> GetComputerStatisticByUsingUnit();
    }

    public class StatisticService : IStatisticService
    {
        private readonly IComputerRepository _computerRepository;

        public StatisticService(IComputerRepository computerRepository)
        {
            _computerRepository = computerRepository;
        }

        public IEnumerable<ComputerStatisticByComputerType> GetComputerStatisticByComputerType()
        {
            return _computerRepository.GetComputerStatisticByComputerType();
        }

        public IEnumerable<ComputerStatisticByProducerType> GetComputerStatisticByProducerType()
        {
            return _computerRepository.GetComputerStatisticByProducerType();
        }

        public IEnumerable<ComputerStatisticByUsingUnit> GetComputerStatisticByUsingUnit()
        {
            return _computerRepository.GetComputerStatisticByUsingUnit();
        }
    }
}