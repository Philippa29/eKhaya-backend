using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.Address;
using eKhaya.Services.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
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

        public AddressAppService(IRepository<Addresses, Guid> addressRepository)
        {
            _addressRepository = addressRepository;
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

        public async Task<List<AddressesDto>> GetAllAddressesAsync()
        {
            var addresses = await _addressRepository.GetAllListAsync();
            return ObjectMapper.Map<List<AddressesDto>>(addresses);
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

