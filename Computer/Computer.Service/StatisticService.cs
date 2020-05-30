using System.Collections.Generic;
using Computer.Common.ViewModels;
using Computer.Data.Repositories;

namespace Computer.Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);

        IEnumerable<ComputerStatisticByComputerType> GetComputerStatisticByComputerType();

        IEnumerable<ComputerStatisticByProducerType> GetComputerStatisticByProducerType();

        IEnumerable<ComputerStatisticByUsingUnit> GetComputerStatisticByUsingUnit();
    }

    public class StatisticService : IStatisticService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IComputerRepository _computerRepository;

        public StatisticService(IOrderRepository orderRepository, IComputerRepository computerRepository)
        {
            _orderRepository = orderRepository;
            _computerRepository = computerRepository;
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            return _orderRepository.GetRevenueStatistic(fromDate, toDate);
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