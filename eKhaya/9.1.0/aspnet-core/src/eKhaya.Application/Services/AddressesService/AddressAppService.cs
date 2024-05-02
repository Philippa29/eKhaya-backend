using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.Address;
using eKhaya.Domain.Properties;
using eKhaya.Services.Dtos;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.AddressesService
{

    public class AddressAppService : ApplicationService, IAddressAppService

    {
        private readonly IRepository<Addresses, Guid> _addressRepository;
        private readonly IRepository<Property , Guid > _propertyRepository;

        public AddressAppService(IRepository<Addresses, Guid> addressRepository, IRepository<Property, Guid> propertyRepository)
        {
            _addressRepository = addressRepository;
            _propertyRepository = propertyRepository;
        }


        public async Task<AddressesDto> CreateAddressAsync(AddressesDto input)
        {
            var address = ObjectMapper.Map<Addresses>(input);
            var newAddress = await _addressRepository.InsertAsync(address);



            return ObjectMapper.Map<AddressesDto>(newAddress);
        }

        public async Task DeleteAddressAsync(Guid id)
        {
            await _addressRepository.DeleteAsync(id);
        }

        public async Task<AddressesDto> GetAddressAsync(Guid id)
        {
            var address = await _addressRepository.GetAsync(id);
            return ObjectMapper.Map<AddressesDto>(address);

        }

        public async Task<List<GetAllAddressesDto>> GetAllAddressesAsync()
        {
            var addressesWithProperty = await _propertyRepository.GetAll()
                .Include(property => property.Address) // Eager loading
                .ToListAsync();

            var addressesDtoList = addressesWithProperty.Select(x => new GetAllAddressesDto
            {
                Id = x.Address.Id, // Assuming Id in GetAllAddressesDto is the address ID
                AddressLine1 = x.Address.AddressLine1,
                AddressLine2 = x.Address.AddressLine2,
                AddressLine3 = x.Address.AddressLine3,
                Suburb = x.Address.Suburb,
                Town = x.Address.Town,
                POBox = x.Address.POBox,
                Latitude = (decimal)x.Address.Latitude,
                Longitude = (decimal)x.Address.Longitude,
                PropertyId = x.Id, // Assuming Id is the property ID
                PropertyName = x.PropertyName // Assuming there's a property name field in the Property entity
            }).ToList();

            return addressesDtoList;
        }





        [HttpPut("updateAddress/{id}")]
        public async Task<AddressesDto> UpdateAddressAsync(AddressesDto input)
        {
            var address = await _addressRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, address);
            var updatedAddress = await _addressRepository.UpdateAsync(address);
            
            return ObjectMapper.Map<AddressesDto>(updatedAddress);
        }

     
    }


}

