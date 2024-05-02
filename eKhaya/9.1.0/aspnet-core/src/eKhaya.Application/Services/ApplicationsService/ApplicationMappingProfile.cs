using AutoMapper;
using eKhaya.Domain.Applications;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ApplicationsService
{
    public class ApplicationMappingProfile : Profile 
    {
        public ApplicationMappingProfile()
        {

            CreateMap<Application, ApplicationsDto>()
                .ForMember(dest => dest.Applicant, opt => opt.MapFrom(src => src.Applicant != null ? src.Applicant.Id : (Guid?)null))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit != null ? src.Unit.Id : (Guid?)null));
            CreateMap<ApplicationsDto, Application>()
                .ForMember(dest => dest.Applicant, opt => opt.Ignore())
                .ForMember(dest => dest.Unit, opt => opt.Ignore());
            CreateMap<CreateApplicationDto, Application>()
                .ForMember(dest => dest.Applicant, opt => opt.Ignore())
                .ForMember(dest => dest.Unit, opt => opt.Ignore());

        }
    }
}
