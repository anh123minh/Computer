using System;
using System.Collections.Generic;
using System.Linq;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;

namespace Computer.Service
{
    public interface IComputerService
    {
        Model.Models.Computer Add(Model.Models.Computer computer);

        void Update(Model.Models.Computer computer);

        Model.Models.Computer Delete(int id);

        IEnumerable<Model.Models.Computer> GetAll();

        IEnumerable<Model.Models.Computer> GetAllPaging(int pageIndex, int pageSize, out int totalRow);

        Model.Models.Computer GetById(int id);

        void Save();

        List<Model.Models.Computer> GetAllPagingWithFilter(int pageIndex, int pageSize, out int totalRow, string filter = "");

        List<Model.Models.Computer> GetAllPagingWithMultiFilters(int pageIndex, int pageSize, out int totalRow,int? computerTypeId, int? deparmanetTypeId, int? producerTypeId, string filter = "");
    }

    public class ComputerService : IComputerService
    {
        private readonly IComputerRepository _computerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ComputerService(IComputerRepository computerRepository, IUnitOfWork unitOfWork)
        {
            this._computerRepository = computerRepository;
            this._unitOfWork = unitOfWork;
        }

        public Model.Models.Computer Add(Model.Models.Computer computer)
        {
            computer.CreatedDate = DateTime.Now;
            //producerType.CreatedBy = ad //Todo: Add CreatedBy
            computer.UpdatedDate = DateTime.Now;
            //producerType.UpdatedBy = ad //Todo: Add CreatedBy
            return _computerRepository.Add(computer);
        }

        public Model.Models.Computer Delete(int id)
        {
            return _computerRepository.Delete(id);
        }

        public IEnumerable<Model.Models.Computer> GetAll()
        {
            return _computerRepository.GetAll();
        }

        public IEnumerable<Model.Models.Computer> GetAllPaging(int pageIndex, int pageSize, out int totalRow)
        {
            return _computerRepository.GetMultiPaging(x => x.Status, out totalRow, pageIndex, pageSize);
        }

        public Model.Models.Computer GetById(int id)
        {
            return _computerRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public List<Model.Models.Computer> GetAllPagingWithFilter(int pageIndex, int pageSize, out int totalRow, string filter ="")
        {
            var query = _computerRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.ComputerCode.Contains(filter) || x.ComputerName.Contains(filter));
            }

            totalRow = query.Count();

            return query.OrderByDescending(x => x.UpdatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Model.Models.Computer> GetAllPagingWithMultiFilters(int pageIndex, int pageSize, out int totalRow, 
            int? computerTypeId, int? deparmanetTypeId, int? producerTypeId, string filter = "")
        {
            var query = _computerRepository.GetAllPagingWithMultiFilters(pageIndex, pageSize, out totalRow,
                computerTypeId, deparmanetTypeId, producerTypeId, filter);

            return query.ToList();
        }

        public void Update(Model.Models.Computer computer)
        {
            computer.UpdatedDate = DateTime.Now;
            //producerType.UpdatedBy = ad //Todo: Add CreatedBy
            _computerRepository.Update(computer);
        }
    }
}
