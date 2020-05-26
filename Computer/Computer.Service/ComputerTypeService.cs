using System.Collections.Generic;
using System.Linq;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;
using Computer.Model.Models;

namespace Computer.Service
{
    public interface IComputerTypeService
    {
        ComputerType Add(ComputerType computer);

        void Update(ComputerType computer);

        ComputerType Delete(int id);

        IEnumerable<ComputerType> GetAll();

        IEnumerable<ComputerType> GetAllPaging(int page, int pageSize, out int totalRow);

        ComputerType GetById(int id);

        void Save();

        List<ComputerType> GetAllPagingWithFilter(int page, int pageSize, out int totalRow, string filter);
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

        public ComputerType Add(ComputerType computer)
        {
            return _computerTypeRepository.Add(computer);
        }

        public ComputerType Delete(int id)
        {
            return _computerTypeRepository.Delete(id);
        }

        public IEnumerable<ComputerType> GetAll()
        {
            return _computerTypeRepository.GetAll();
        }

        public IEnumerable<ComputerType> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _computerTypeRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public ComputerType GetById(int id)
        {
            return _computerTypeRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public List<ComputerType> GetAllPagingWithFilter(int page, int pageSize, out int totalRow, string filter = "")
        {
            var query = _computerTypeRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.ComputerTypeCode.Contains(filter) || x.ComputerTypeName.Contains(filter));
            }
            
            totalRow = query.Count();

            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Update(ComputerType computer)
        {
            _computerTypeRepository.Update(computer);
        }
    }
}
