using System.Collections.Generic;
using System.Linq;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;
using Computer.Model.Models;

namespace Computer.Service
{
    public interface IProducerTypeService
    {
        ProducerType Add(ProducerType producer);

        void Update(ProducerType producer);

        ProducerType Delete(int id);

        IEnumerable<ProducerType> GetAll();

        IEnumerable<ProducerType> GetAllPaging(int page, int pageSize, out int totalRow);

        ProducerType GetById(int id);

        void Save();

        List<ProducerType> GetAllPagingWithFilter(int page, int pageSize, out int totalRow, string filter);
    }

    public class ProducerTypeService : IProducerTypeService
    {
        private readonly IProducerTypeRepository _producerTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProducerTypeService(IProducerTypeRepository producerTypeRepository, IUnitOfWork unitOfWork)
        {
            this._producerTypeRepository = producerTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProducerType Add(ProducerType producer)
        {
            return _producerTypeRepository.Add(producer);
        }

        public ProducerType Delete(int id)
        {
            return _producerTypeRepository.Delete(id);
        }

        public IEnumerable<ProducerType> GetAll()
        {
            return _producerTypeRepository.GetAll();
        }

        public IEnumerable<ProducerType> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _producerTypeRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public ProducerType GetById(int id)
        {
            return _producerTypeRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public List<ProducerType> GetAllPagingWithFilter(int page, int pageSize, out int totalRow, string filter = "")
        {
            var query = _producerTypeRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.ProducerTypeCode.Contains(filter) || x.ProducerTypeName.Contains(filter));
            }

            totalRow = query.Count();

            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Update(ProducerType producer)
        {
            _producerTypeRepository.Update(producer);
        }
    }
}
