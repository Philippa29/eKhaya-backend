using AutoMapper;
using eKhaya.Domain.Units;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.UnitsService
{
    public class UnitsMappingProfile : Profile
    {
        public UnitsMappingProfile()
        {
            CreateMap<Unit, UnitsDto>()
                .ForMember(dest => dest.AgentID, opt => opt.MapFrom(src => src.AgentID != null ? src.AgentID.Id : Guid.Empty))
                .ForMember(dest => dest.PropertyID, opt => opt.MapFrom(src => src.PropertyID != null ? src.PropertyID.Id : Guid.Empty));
            CreateMap<UnitsDto, Unit>()
                .ForMember(dest => dest.AgentID, opt => opt.Ignore());
            CreateMap<CreateUnitsDto, Unit>()
               .ForMember(dest => dest.AgentID, opt => opt.Ignore());
            CreateMap<Unit, UpdateUnitsDto>()
                
                .ForMember(dest => dest.AgentID, opt => opt.MapFrom(src => src.AgentID));
            CreateMap<UpdateUnitsDto, Unit>()
                .ForMember(dest => dest.AgentID, opt => opt.Ignore()); 
        }
    }
}
