using AutoMapper;
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
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.Unit != null ? src.Unit.Id : (Guid?)null))
                .ForMember(dest => dest.AmenityId, opt => opt.MapFrom(src => src.Amenity != null ? src.Amenity.Id : (Guid?)null));
            CreateMap<Dtos.UnitsAmenitiesDto, UnitsAmenities>()
                .ForMember(dest => dest.Unit, opt => opt.Ignore())
                .ForMember(dest => dest.Amenity, opt => opt.Ignore());
            
        }
    }
}
