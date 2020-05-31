using System;
using System.Collections.Generic;
using System.Linq;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;
using Computer.Model.Models;

namespace Computer.Service
{
    public interface IComputerUsingHistoryService
    {
        bool CheckExistedId(int id);

        ComputerUsingHistory Add(ComputerUsingHistory computerUsingHistory);

        void Update(ComputerUsingHistory computerUsingHistory);

        ComputerUsingHistory Delete(int id);

        IEnumerable<ComputerUsingHistory> GetAll();

        IEnumerable<ComputerUsingHistory> GetAllPaging(int pageIndex, int pageSize, out int totalRow);

        ComputerUsingHistory GetById(int id);

        void Save();

        List<ComputerUsingHistory> GetAllPagingWithFilter(int pageIndex, int pageSize, out int totalRow, string filter);

        List<ComputerUsingHistory> GetAllPagingWithFilterDeparmentTypeId(int pageIndex, int pageSize, out int totalRow, int? deparmentTypeId, string filter);
    }

    public class ComputerUsingHistoryService : IComputerUsingHistoryService
    {
        private readonly IComputerUsingHistoryRepository _computerUsingHistoryRepository;
        private readonly IComputerRepository _computerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ComputerUsingHistoryService(IComputerUsingHistoryRepository computerTypeRepository, IComputerRepository computerRepository, IUnitOfWork unitOfWork)
        {
            this._computerUsingHistoryRepository = computerTypeRepository;
            this._computerRepository = computerRepository;
            this._unitOfWork = unitOfWork;
        }

        public bool CheckExistedId(int id)
        {
            return _computerUsingHistoryRepository.CheckContains(x => x.ComputerUsingHistoryId == id);
        }

        public ComputerUsingHistory Add(ComputerUsingHistory computerUsingHistory)
        {
            computerUsingHistory.CreatedDate = DateTime.Now;
            //producerType.CreatedBy = ad //Todo: Add CreatedBy
            computerUsingHistory.UpdatedDate = DateTime.Now;
            //producerType.UpdatedBy = ad //Todo: Add CreatedBy
            return _computerUsingHistoryRepository.Add(computerUsingHistory);
        }

        public ComputerUsingHistory Delete(int id)
        {
            return _computerUsingHistoryRepository.Delete(id);
        }

        public IEnumerable<ComputerUsingHistory> GetAll()
        {
            return _computerUsingHistoryRepository.GetAll();
        }

        public IEnumerable<ComputerUsingHistory> GetAllPaging(int pageIndex, int pageSize, out int totalRow)
        {
            return _computerUsingHistoryRepository.GetMultiPaging(x => x.Status, out totalRow, pageIndex, pageSize);
        }

        public ComputerUsingHistory GetById(int id)
        {
            var computerUsingHistory = _computerUsingHistoryRepository.GetSingleByCondition(x => x.ComputerUsingHistoryId == id, new [] { "Computer" });
            computerUsingHistory.Computer = _computerRepository.GetSingleByCondition(x => x.ComputerId == computerUsingHistory.ComputerId, new [] { "DeparmentType" });
            return computerUsingHistory;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public List<ComputerUsingHistory> GetAllPagingWithFilter(int pageIndex, int pageSize, out int totalRow, string filter = "")
        {
            var query = _computerUsingHistoryRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Computer.ComputerCode.Contains(filter) || x.Computer.ComputerName.Contains(filter));
            }

            totalRow = query.Count();

            return query.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<ComputerUsingHistory> GetAllPagingWithFilterDeparmentTypeId(int pageIndex, int pageSize, out int totalRow, int? deparmentTypeId,
            string filter = "")
        {
            var query = _computerUsingHistoryRepository.GetAllPagingWithFilterDeparmentTypeId(pageIndex, pageSize, out totalRow, deparmentTypeId, filter);

            return query.ToList();
        }

        public void Update(ComputerUsingHistory computerUsingHistory)
        {
            computerUsingHistory.UpdatedDate = DateTime.Now;
            //producerType.UpdatedBy = ad //Todo: Add CreatedBy
            _computerUsingHistoryRepository.Update(computerUsingHistory);
        }
    }
}
