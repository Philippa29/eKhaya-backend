using AutoMapper;
using eKhaya.Domain.Properties;
using eKhaya.Domain.PropertyAmenities;
using eKhaya.Domain.UnitsAmenities;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.PropertyAmenitiesService
{
    public class PropertyAmenitiesMappingProfile :Profile
    {
        public PropertyAmenitiesMappingProfile()
        {
            CreateMap<PropertyAmenity, PropertyAmenitiesDto>()
               .ForMember(dest => dest.Property, opt => opt.MapFrom(src => src.Property != null ? src.Property.Id : (Guid?)null))
               .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenity != null ? src.Amenity.Id : (Guid?)null));
            CreateMap<PropertyAmenitiesDto, PropertyAmenity>()
                .ForMember(dest => dest.Property, opt => opt.Ignore())
                .ForMember(dest => dest.Amenity, opt => opt.Ignore());

            CreateMap<Property, ViewPropertyDto>()
              .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.PropertyName))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
