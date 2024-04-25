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
            CreateMap<Application, ApplicationsDto>();
            CreateMap<ApplicationsDto, Application>();
        }
    }
}
