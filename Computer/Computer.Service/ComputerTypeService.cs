using System.Collections.Generic;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;

namespace Computer.Service
{
    public interface IComputerTypeService
    {
        Model.Models.ComputerType Add(Model.Models.ComputerType computer);

        void Update(Model.Models.ComputerType computer);

        Model.Models.ComputerType Delete(int id);

        IEnumerable<Model.Models.ComputerType> GetAll();

        IEnumerable<Model.Models.ComputerType> GetAllPaging(int page, int pageSize, out int totalRow);

        Model.Models.ComputerType GetById(int id);

        void Save();
    }

    public class ComputerTypeService : IComputerTypeService
    {
        private IComputerTypeRepository _computerTypeRepository;
        private IUnitOfWork _unitOfWork;

        public ComputerTypeService(IComputerTypeRepository computerTypeRepository, IUnitOfWork unitOfWork)
        {
            this._computerTypeRepository = computerTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public Model.Models.ComputerType Add(Model.Models.ComputerType computer)
        {
            return _computerTypeRepository.Add(computer);
        }

        public Model.Models.ComputerType Delete(int id)
        {
            return _computerTypeRepository.Delete(id);
        }

        public IEnumerable<Model.Models.ComputerType> GetAll()
        {
            return _computerTypeRepository.GetAll();
        }

        public IEnumerable<Model.Models.ComputerType> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _computerTypeRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Model.Models.ComputerType GetById(int id)
        {
            return _computerTypeRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Model.Models.ComputerType computer)
        {
            _computerTypeRepository.Update(computer);
        }
    }
}
