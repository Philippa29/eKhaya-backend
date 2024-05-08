using AutoMapper;
using eKhaya.Authorization.Users;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ProjectManagerService
{
    public class PropertyManagerMappingProfile : Profile
    {
        public PropertyManagerMappingProfile()
        {
            CreateMap<PropertyManager, PropertyManagerDto>();

            //.ForMember(e => e.RoleNames, d => d.Ignore());
            CreateMap<CreatePropertyManagerDto, PropertyManager>();
            CreateMap<CreatePropertyManagerDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());
            CreateMap<PropertyManagerDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());
            CreateMap<UpdatePropertyManagerDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());
            CreateMap<UpdatePropertyManagerDto, PropertyManager>();
            //.ForMember(x => x.Roles, m  => m.MapFrom(x => x.RoleNames )); //ignores keys 

        }
    }
}
