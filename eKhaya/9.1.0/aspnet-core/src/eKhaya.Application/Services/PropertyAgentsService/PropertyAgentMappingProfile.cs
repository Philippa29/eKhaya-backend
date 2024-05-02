using AutoMapper;
using eKhaya.Domain.PropertyAmenities;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.PropertyAgentsService
{
    public class PropertyAgentMappingProfile : Profile
    {
        public PropertyAgentMappingProfile()
        {
            CreateMap<PropertyAmenity, PropertyAmenitiesDto>()
               .ForMember(dest => dest.Property, opt => opt.MapFrom(src => src.Property != null ? src.Property.Id : (Guid?)null))
               .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenity != null ? src.Amenity.Id : (Guid?)null));
            CreateMap<PropertyAmenitiesDto, PropertyAmenity>()
                .ForMember(dest => dest.Property, opt => opt.Ignore())
                .ForMember(dest => dest.Amenity, opt => opt.Ignore());

        }
    }
}
