using AutoMapper;
using eKhaya.Domain.Properties;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.PropertyService
{
    public class PropertyMappingProfile : Profile
    {
        public PropertyMappingProfile()
        {
            CreateMap<Property, PropertyDto>(); 
                

            CreateMap<PropertyDto, Property>();


            CreateMap<UpdatePropertyDto, Property>(); 



        }
    }
}
