using AutoMapper;
using eKhaya.Domain.Address;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.AddressesService
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<AddressesDto, Addresses>();
            CreateMap<Addresses, AddressesDto>();
        }
    }
}
