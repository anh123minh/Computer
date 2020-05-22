using System.Collections.Generic;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;

namespace Computer.Service
{
    public interface IDeparmentTypeService
    {
        Model.Models.DeparmentType Add(Model.Models.DeparmentType deparmentType);

        void Update(Model.Models.DeparmentType deparmentType);

        Model.Models.DeparmentType Delete(int id);

        IEnumerable<Model.Models.DeparmentType> GetAll();

        IEnumerable<Model.Models.DeparmentType> GetAllPaging(int page, int pageSize, out int totalRow);

        Model.Models.DeparmentType GetById(int id);

        void Save();
    }

    public class DeparmentTypeService : IDeparmentTypeService
    {
        private IDeparmentTypeRepository _deparmentTypeRepository;
        private IUnitOfWork _unitOfWork;

        public DeparmentTypeService(IDeparmentTypeRepository deparmentTypeRepository, IUnitOfWork unitOfWork)
        {
            this._deparmentTypeRepository = deparmentTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public Model.Models.DeparmentType Add(Model.Models.DeparmentType deparmentType)
        {
            return _deparmentTypeRepository.Add(deparmentType);
        }

        public Model.Models.DeparmentType Delete(int id)
        {
            return _deparmentTypeRepository.Delete(id);
        }

        public IEnumerable<Model.Models.DeparmentType> GetAll()
        {
            return _deparmentTypeRepository.GetAll();
        }

        public IEnumerable<Model.Models.DeparmentType> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _deparmentTypeRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Model.Models.DeparmentType GetById(int id)
        {
            return _deparmentTypeRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Model.Models.DeparmentType deparmentType)
        {
            _deparmentTypeRepository.Update(deparmentType);
        }
    }
}
