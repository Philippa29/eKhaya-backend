using AutoMapper;
using eKhaya.Domain.Applications;
using eKhaya.Domain.Users;
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
                .ForMember(dest => dest.Property , opt => opt.MapFrom(src => src.Property != null ? src.Property.Id : (Guid?)null));

            CreateMap<Application, GetApplicationsDto>()
                .ForMember(dest => dest.Applicant, opt => opt.MapFrom(src => src.Applicant != null ? src.Applicant.Id : (Guid?)null))
                .ForMember(dest => dest.Property, opt => opt.MapFrom(src => src.Property != null ? src.Property.Id : (Guid?)null));
                

            CreateMap<ApplicationsDto, Application>()
            .ForMember(dest => dest.Property, opt => opt.MapFrom(src => src.Property != null ? src.Property : (Guid?)null))
             .ForMember(dest => dest.Applicant, opt => opt.MapFrom(src => src.Applicant != null ? new Applicant { Id = src.Applicant } : (Applicant)null));

            CreateMap<CreateApplicationDto, Application>()
                .ForMember(dest => dest.Applicant, opt => opt.Ignore());



        }
    }
}
