using AutoMapper;
using eKhaya.Authorization.Users;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ApplicantService
{
    public class ApplicantMappingProfile : Profile 
    {
        public ApplicantMappingProfile()
        {
            CreateMap<Applicant, ApplicantDto>();

            //.ForMember(e => e.RoleNames, d => d.Ignore());
            CreateMap<CreateApplicantDto, Applicant>();
            CreateMap<CreateApplicantDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());
            CreateMap<ApplicantDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());
            CreateMap<UpdateApplicantDto, User>()
                .ForMember(e => e.Id, d => d.Ignore());
            CreateMap<UpdateApplicantDto, Applicant>();
            //.ForMember(x => x.Roles, m  => m.MapFrom(x => x.RoleNames )); //ignores keys 
        }
    }
}
