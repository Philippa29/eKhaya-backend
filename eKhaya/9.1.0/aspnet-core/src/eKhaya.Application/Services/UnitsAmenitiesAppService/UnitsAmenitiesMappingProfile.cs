using AutoMapper;
using eKhaya.Domain.ENums;
using eKhaya.Domain.UnitsAmenities;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.UnitsAmenitiesAppService
{
    public class UnitsAmenitiesMappingProfile :Profile 
    {
        public UnitsAmenitiesMappingProfile()
        {
            CreateMap<UnitsAmenities, UnitsAmenitiesDto>()
                .ForMember(dest => dest.Property, opt => opt.MapFrom(src => src.Property != null ? src.Property.Id : Guid.Empty))
                .ForMember(dest => dest.UnitType, opt => opt.MapFrom(src => src.UnitType))
                .ForMember(dest => dest.Amenity, opt => opt.MapFrom(src => src.Amenity != null ? src.Amenity.Id : Guid.Empty));

            CreateMap<UnitsAmenitiesDto, UnitsAmenities>()
                .ForMember(dest => dest.Property, opt => opt.Ignore())
                .ForMember(dest => dest.UnitType, opt => opt.MapFrom(src => src.UnitType))
                .ForMember(dest => dest.Amenity, opt => opt.Ignore());

            CreateMap<UnitsAmenities, ViewUnitsPerPropertyDto>()
                .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(src => src.Property != null ? src.Property.Id : Guid.Empty))
                .ForMember(dest => dest.UnitType, opt => opt.MapFrom(src => src.UnitType))
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => new List<Guid> { src.Amenity != null ? src.Amenity.Id : Guid.Empty })); 
                
        }
    }
}
