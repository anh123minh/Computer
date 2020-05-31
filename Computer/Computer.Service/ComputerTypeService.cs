using System;
using System.Collections.Generic;
using System.Linq;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;
using Computer.Model.Models;

namespace Computer.Service
{
    public interface IComputerTypeService
    {
        bool CheckExistedId(int id);

        ComputerType Add(ComputerType computerType);

        void Update(ComputerType computerType);

        ComputerType Delete(int id);

        IEnumerable<ComputerType> GetAll();

        IEnumerable<ComputerType> GetAllPaging(int pageIndex, int pageSize, out int totalRow);

        ComputerType GetById(int id);

        void Save();

        List<ComputerType> GetAllPagingWithFilter(int pageIndex, int pageSize, out int totalRow, string filter);
    }

    public class ComputerTypeService : IComputerTypeService
    {
        private readonly IComputerTypeRepository _computerTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ComputerTypeService(IComputerTypeRepository computerTypeRepository, IUnitOfWork unitOfWork)
        {
            this._computerTypeRepository = computerTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public bool CheckExistedId(int id)
        {
            return _computerTypeRepository.CheckContains(x => x.ComputerTypeId == id);
        }

        public ComputerType Add(ComputerType computerType)
        {
            computerType.CreatedDate = DateTime.Now;
            //computerType.CreatedBy = ad //Todo: Add CreatedBy
            computerType.UpdatedDate = DateTime.Now;
            //computerType.UpdatedBy = ad //Todo: Add CreatedBy
            return _computerTypeRepository.Add(computerType);
        }

        public ComputerType Delete(int id)
        {
            return _computerTypeRepository.Delete(id);
        }

        public IEnumerable<ComputerType> GetAll()
        {
            return _computerTypeRepository.GetAll();
        }

        public IEnumerable<ComputerType> GetAllPaging(int pageIndex, int pageSize, out int totalRow)
        {
            return _computerTypeRepository.GetMultiPaging(x => x.Status, out totalRow, pageIndex, pageSize);
        }

        public ComputerType GetById(int id)
        {
            return _computerTypeRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public List<ComputerType> GetAllPagingWithFilter(int pageIndex, int pageSize, out int totalRow, string filter = "")
        {
            var query = _computerTypeRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.ComputerTypeCode.Contains(filter) || x.ComputerTypeName.Contains(filter));
            }
            
            totalRow = query.Count();

            return query.OrderByDescending(x => x.UpdatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Update(ComputerType computerType)
        {
            computerType.UpdatedDate = DateTime.Now;
            //computerType.UpdatedBy = ad //Todo: Add CreatedBy
            _computerTypeRepository.Update(computerType);
        }
    }
}
