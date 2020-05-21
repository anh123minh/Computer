using System.Collections.Generic;
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

        IEnumerable<Model.Models.Computer> GetAllPaging(int page, int pageSize, out int totalRow);

        Model.Models.Computer GetById(int id);

        void Save();
    }

    public class ComputerService : IComputerService
    {
        private IComputerRepository _computerRepository;
        private IUnitOfWork _unitOfWork;

        public ComputerService(IComputerRepository computerRepository, IUnitOfWork unitOfWork)
        {
            this._computerRepository = computerRepository;
            this._unitOfWork = unitOfWork;
        }

        public Model.Models.Computer Add(Model.Models.Computer computer)
        {
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

        public IEnumerable<Model.Models.Computer> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _computerRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Model.Models.Computer GetById(int id)
        {
            return _computerRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Model.Models.Computer computer)
        {
            _computerRepository.Update(computer);
        }
    }
}
