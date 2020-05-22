using AutoMapper;
using Computer.Model.Models;
using Computer.Models;

namespace Computer.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Computer.Model.Models.Computer, ComputerViewModel>().MaxDepth(2);
                cfg.CreateMap<ComputerType, ComputerTypeViewModel>().MaxDepth(2);
                cfg.CreateMap<ProducerType, ProducerTypeViewModel>().MaxDepth(2);
                cfg.CreateMap<DeparmentType, DeparmentTypeViewModel>().MaxDepth(2);
                cfg.CreateMap<ComputerUsingHistory, ComputerUsingHistoryViewModel>().MaxDepth(2);
                //cfg.CreateMap<AppRole, ApplicationRoleViewModel>().MaxDepth(2);
                //cfg.CreateMap<AppUser, AppUserViewModel>().MaxDepth(2);
                //cfg.CreateMap<Function, FunctionViewModel>().MaxDepth(2);
                //cfg.CreateMap<Permission, PermissionViewModel>().MaxDepth(2);
                //cfg.CreateMap<Announcement, AnnouncementViewModel>().MaxDepth(2);
                //cfg.CreateMap<AnnouncementUser, AnnouncementUserViewModel>().MaxDepth(2);
            });
        }
    }
}