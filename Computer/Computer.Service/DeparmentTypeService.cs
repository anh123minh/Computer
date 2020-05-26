using System.Collections.Generic;
using System.Linq;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;
using Computer.Model.Models;

namespace Computer.Service
{
    public interface IDeparmentTypeService
    {
        DeparmentType Add(DeparmentType deparmentType);

        void Update(DeparmentType deparmentType);

        DeparmentType Delete(int id);

        IEnumerable<DeparmentType> GetAll();

        IEnumerable<DeparmentType> GetAllPaging(int page, int pageSize, out int totalRow);

        DeparmentType GetById(int id);

        void Save();

        List<DeparmentType> GetAllPagingWithFilter(int page, int pageSize, out int totalRow, string filter);
    }

    public class DeparmentTypeService : IDeparmentTypeService
    {
        private readonly IDeparmentTypeRepository _deparmentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeparmentTypeService(IDeparmentTypeRepository deparmentTypeRepository, IUnitOfWork unitOfWork)
        {
            this._deparmentTypeRepository = deparmentTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public DeparmentType Add(DeparmentType deparmentType)
        {
            return _deparmentTypeRepository.Add(deparmentType);
        }

        public DeparmentType Delete(int id)
        {
            return _deparmentTypeRepository.Delete(id);
        }

        public IEnumerable<DeparmentType> GetAll()
        {
            return _deparmentTypeRepository.GetAll();
        }

        public IEnumerable<DeparmentType> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _deparmentTypeRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public DeparmentType GetById(int id)
        {
            return _deparmentTypeRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public List<DeparmentType> GetAllPagingWithFilter(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _deparmentTypeRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.DeparmentTypeCode.Contains(filter) || x.DeparmentTypeName.Contains(filter));
            }

            totalRow = query.Count();

            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Update(DeparmentType deparmentType)
        {
            _deparmentTypeRepository.Update(deparmentType);
        }
    }
}
