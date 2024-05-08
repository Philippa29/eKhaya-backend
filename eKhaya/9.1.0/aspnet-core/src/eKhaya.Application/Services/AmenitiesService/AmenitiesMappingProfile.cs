using AutoMapper;
using eKhaya.Domain.Amenities;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.AmenitiesService
{
    public class AmenitiesMappingProfile : Profile
    {
        public AmenitiesMappingProfile()
        {
            CreateMap<Amenity, AmenitiesDto>(); 

            CreateMap<AmenitiesDto, Amenity>();
        }
    }
}
