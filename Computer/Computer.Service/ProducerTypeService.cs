using System.Collections.Generic;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;

namespace Computer.Service
{
    public interface IProducerTypeService
    {
        Model.Models.ProducerType Add(Model.Models.ProducerType producer);

        void Update(Model.Models.ProducerType producer);

        Model.Models.ProducerType Delete(int id);

        IEnumerable<Model.Models.ProducerType> GetAll();

        IEnumerable<Model.Models.ProducerType> GetAllPaging(int page, int pageSize, out int totalRow);

        Model.Models.ProducerType GetById(int id);

        void Save();
    }

    public class ProducerTypeService : IProducerTypeService
    {
        private IProducerTypeRepository _producerTypeRepository;
        private IUnitOfWork _unitOfWork;

        public ProducerTypeService(IProducerTypeRepository producerTypeRepository, IUnitOfWork unitOfWork)
        {
            this._producerTypeRepository = producerTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public Model.Models.ProducerType Add(Model.Models.ProducerType producer)
        {
            return _producerTypeRepository.Add(producer);
        }

        public Model.Models.ProducerType Delete(int id)
        {
            return _producerTypeRepository.Delete(id);
        }

        public IEnumerable<Model.Models.ProducerType> GetAll()
        {
            return _producerTypeRepository.GetAll();
        }

        public IEnumerable<Model.Models.ProducerType> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _producerTypeRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Model.Models.ProducerType GetById(int id)
        {
            return _producerTypeRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Model.Models.ProducerType producer)
        {
            _producerTypeRepository.Update(producer);
        }
    }
}
