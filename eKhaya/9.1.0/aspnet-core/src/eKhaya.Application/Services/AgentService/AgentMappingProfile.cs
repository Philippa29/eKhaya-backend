using AutoMapper;
using eKhaya.Authorization.Users;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.AgentService
{
    public class AgentMappingProfile : Profile
    {
        public AgentMappingProfile()
        {
            CreateMap<Agent, AgentDto>();

            //.ForMember(e => e.RoleNames, d => d.Ignore());
            CreateMap<CreateAgentDto, Agent>();
            CreateMap<CreateAgentDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());
            CreateMap<AgentDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());
            //.ForMember(x => x.Roles, m  => m.MapFrom(x => x.RoleNames )); //ignores keys 


        }
    }
}
