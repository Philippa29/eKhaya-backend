using AutoMapper;
using eKhaya.Domain.Leases;
using eKhaya.Services.Dtos;

namespace eKhaya.Services.MappingProfiles
{
    public class LeaseMappingProfile : Profile
    {
        public LeaseMappingProfile()
        {
            CreateMap<LeaseDto, Lease>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore mapping ID from LeaseDto to Lease
            CreateMap<Lease, LeaseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)); // Map ID from Lease to LeaseDto
        }
    }
}
