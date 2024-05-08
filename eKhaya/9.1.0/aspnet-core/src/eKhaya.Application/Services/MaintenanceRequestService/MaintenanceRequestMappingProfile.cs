using AutoMapper;
using eKhaya.Domain.MaintenanceRequests;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.MaintenanceRequestService
{
    public class MaintenanceRequestMappingProfile : Profile
    {
        public MaintenanceRequestMappingProfile()
        {
            CreateMap<MaintenanceRequest, MaintenanceRequestDto>()
               .ForMember(dest => dest.Tenant, opt => opt.MapFrom(src => src.Tenant != null ? src.Tenant.Id : (Guid?)null)); 
               
              
            CreateMap<MaintenanceRequestDto, MaintenanceRequest>()
                .ForMember(dest => dest.Tenant, opt => opt.Ignore());
            CreateMap<CreateMaintenanceRequestDto, MaintenanceRequest>(); 
        }

    }
}
