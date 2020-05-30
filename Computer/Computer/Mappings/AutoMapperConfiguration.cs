using AutoMapper;
using Computer.Model.Models;
using Computer.Models.Common;
using Computer.Models.Computer;
using Computer.Models.System;

namespace Computer.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Model.Models.Computer, ComputerViewModel>().MaxDepth(2);
                cfg.CreateMap<Model.Models.Computer, ComputerDetailViewModel>()
                    .ForMember(dest => dest.ComputerTypeName, opts => opts.MapFrom(src => src.ComputerType.ComputerTypeName))
                    .ForMember(dest => dest.DeparmentTypeName, opts => opts.MapFrom(src => src.DeparmentType.DeparmentTypeName))
                    .ForMember(dest => dest.ProducerTypeName, opts => opts.MapFrom(src => src.ProducerType.ProducerTypeName));

                cfg.CreateMap<ComputerType, ComputerTypeViewModel>().MaxDepth(2);
                cfg.CreateMap<ComputerType, ComputerTypeSelectListViewModel>()
                    .ForMember(dest => dest.ComputerTypeId, opts => opts.MapFrom(src => src.ComputerTypeId))
                    .ForMember(dest => dest.ComputerTypeCode, opts => opts.MapFrom(src => src.ComputerTypeCode))
                    .ForMember(dest => dest.ComputerTypeName, opts => opts.MapFrom(src => src.ComputerTypeName));
                
                cfg.CreateMap<ProducerType, ProducerTypeViewModel>().MaxDepth(2);
                cfg.CreateMap<ProducerType, ProducerTypeSelectListViewModel>()
                    .ForMember(dest => dest.ProducerTypeId, opts => opts.MapFrom(src => src.ProducerTypeId))
                    .ForMember(dest => dest.ProducerTypeCode, opts => opts.MapFrom(src => src.ProducerTypeCode))
                    .ForMember(dest => dest.ProducerTypeName, opts => opts.MapFrom(src => src.ProducerTypeName));

                cfg.CreateMap<DeparmentType, DeparmentTypeViewModel>().MaxDepth(2);
                cfg.CreateMap<DeparmentType, DeparmentTypeSelectListViewModel>()
                    .ForMember(dest => dest.DeparmentTypeId, opts => opts.MapFrom(src => src.DeparmentTypeId))
                    .ForMember(dest => dest.DeparmentTypeCode, opts => opts.MapFrom(src => src.DeparmentTypeCode))
                    .ForMember(dest => dest.DeparmentTypeName, opts => opts.MapFrom(src => src.DeparmentTypeName));

                cfg.CreateMap<ComputerUsingHistory, ComputerUsingHistoryViewModel>().MaxDepth(2);
                cfg.CreateMap<ComputerUsingHistory, ComputerUsingHistoryDetailViewModel>()
                    .ForMember(dest => dest.ComputerId, opts => opts.MapFrom(src => src.Computer.ComputerId))
                    .ForMember(dest => dest.ComputerCode, opts => opts.MapFrom(src => src.Computer.ComputerCode))
                    .ForMember(dest => dest.ComputerName, opts => opts.MapFrom(src => src.Computer.ComputerName))
                    .ForMember(dest => dest.DeparmentTypeId, opts => opts.MapFrom(src => src.Computer.DeparmentType.DeparmentTypeId))
                    .ForMember(dest => dest.DeparmentTypeCode, opts => opts.MapFrom(src => src.Computer.DeparmentType.DeparmentTypeCode))
                    .ForMember(dest => dest.DeparmentTypeName, opts => opts.MapFrom(src => src.Computer.DeparmentType.DeparmentTypeName));

                cfg.CreateMap<AppRole, ApplicationRoleViewModel>();
                cfg.CreateMap<AppUser, AppUserViewModel>();
                cfg.CreateMap<Function, FunctionViewModel>();
                cfg.CreateMap<Permission, PermissionViewModel>();
                cfg.CreateMap<Announcement, AnnouncementViewModel>();
                cfg.CreateMap<AnnouncementUser, AnnouncementUserViewModel>();
            });
        }
    }
}