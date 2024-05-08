using AutoMapper;
using eKhaya.Authorization.Users;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ResidentService
{
    public class ResidentMappingProfile : Profile 
    {
        public ResidentMappingProfile()
        {
            CreateMap<Resident, ResidentDto>();

            //.ForMember(e => e.RoleNames, d => d.Ignore());
            CreateMap<CreateResidentDto, Resident>();
            CreateMap<CreateResidentDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());
            CreateMap<ResidentDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());

            CreateMap<UpdateResidentDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());
            CreateMap<UpdateResidentDto, Agent>();
        }
    }
}
