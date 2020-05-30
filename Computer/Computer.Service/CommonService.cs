using Computer.Data.Infrastructure;
using Computer.Data.Repositories;
using Computer.Model.Models;

namespace Computer.Service
{
    public interface ICommonService
    {
        SystemConfig GetSystemConfig(string code);
    }

    public class CommonService : ICommonService
    {
        private readonly ISystemConfigRepository _systemConfigRepository;
        private IUnitOfWork _unitOfWork;

        public CommonService(ISystemConfigRepository systemConfigRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _systemConfigRepository = systemConfigRepository;
        }

        public SystemConfig GetSystemConfig(string code)
        {
            return _systemConfigRepository.GetSingleByCondition(x => x.Code == code);
        }
    }
}