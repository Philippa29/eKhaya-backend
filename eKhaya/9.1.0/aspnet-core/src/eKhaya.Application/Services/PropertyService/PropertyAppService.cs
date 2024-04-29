using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using eKhaya.Domain.Address;
using eKhaya.Domain.AgentsProperty;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.Properties;
using eKhaya.Domain.PropertyAmenities;
using eKhaya.Domain.Users;
using eKhaya.Services.AddressesService;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.PropertyService
{
    public class PropertyAppService : ApplicationService , IPropertyAppService
    {
        private readonly IRepository<Property, Guid> _propertyRepository;
        private readonly IRepository<Agent, Guid> _agentRepository;
        private readonly IRepository<Amenity, Guid> _amenityRepository;
        private readonly IRepository<Addresses, Guid> _addressesRepository;
        private readonly IRepository<PropertyAmenity, Guid> _propertyAmenityRepository;
        private readonly IRepository<AgentProperty, Guid> _agentPropertyRepository;
        private readonly IRepository<PropertyManager, Guid> _propertyManagerRepository;
        private readonly IAddressAppService _addresses;

        public PropertyAppService(IAddressAppService addresses, IRepository<PropertyManager, Guid> propertyManagerRepository, IRepository<Addresses, Guid> addressesRepository , IRepository<Property, Guid> propertyRepository, IRepository<Agent, Guid> agentRepository, IRepository<Amenity, Guid> amenityRepository, IRepository<PropertyAmenity, Guid> propertyAmenityRepository, IRepository<AgentProperty, Guid> agentPropertyRepository)
        {
            _propertyRepository = propertyRepository;
            _agentRepository = agentRepository;
            _amenityRepository = amenityRepository;
            _propertyAmenityRepository = propertyAmenityRepository;
            _agentPropertyRepository = agentPropertyRepository;
            _addressesRepository = addressesRepository;
            _propertyManagerRepository = propertyManagerRepository;
            _addresses = addresses;
        }

        [AbpAuthorize("Pages.PropertyManager")]
        public async Task<PropertyDto> CreatePropertyAsync(PropertyDto propertyDto)
        {
            // Create a new Address entity
            var newAddress = new AddressesDto
            {
                AddressLine1 = propertyDto.AddressLine1,
                AddressLine2 = propertyDto.AddressLine2,
                AddressLine3 = propertyDto.AddressLine3,
                Suburb = propertyDto.Suburb,
                Town = propertyDto.Town,
                POBox = propertyDto.POBox,
                Latitude = propertyDto.Latitude,
                Longitude = propertyDto.Longitude
            };

             

            var addresses = await _addresses.CreateAddressAsync(newAddress);

            await CurrentUnitOfWork.SaveChangesAsync();
            // Retrieve the inserted address
            var getAddress = await _addressesRepository.GetAsync(addresses.Id);

            // Retrieve the property manager
            var propertyManager = await _propertyManagerRepository.GetAsync(propertyDto.PropertyManagerId);
            if (propertyManager == null)
            {
                throw new Exception("Property Manager not found");
            }

            // Create a new Property entity
            var property = new Property
            {
                Address = getAddress,
                Size = propertyDto.Size,
                PropertyManager = propertyManager,
            };
            await _propertyRepository.InsertAsync(property);

            // Assign amenities to the property
            foreach (var amenityId in propertyDto.AmenityIds)
            {
                var amenity = await _amenityRepository.GetAsync(amenityId);
                if (amenity == null)
                {
                    throw new Exception($"Amenity with ID {amenityId} not found");
                }

                var propertyAmenity = new PropertyAmenity
                {
                    Property = property,
                    Amenity = amenity
                };

                await _propertyAmenityRepository.InsertAsync(propertyAmenity);
            }

            // Assign agents to the property
            foreach (var agentId in propertyDto.AgentIds)
            {
                var agent = await _agentRepository.GetAsync(agentId);
                if (agent == null)
                {
                    throw new Exception($"Agent with ID {agentId} not found");
                }

                var agentProperty = new AgentProperty
                {
                    Property = property,
                    Agent = agent
                };

                await _agentPropertyRepository.InsertAsync(agentProperty);
            }

            // Save changes to the database
            await CurrentUnitOfWork.SaveChangesAsync();

            // Map and return the created property DTO
            return ObjectMapper.Map<PropertyDto>(property);
        }


        public async Task DeletePropertyAsync(Guid id)
        {
            await _propertyRepository.DeleteAsync(id);
        }

        public async Task<List<PropertyDto>> GetAllPropertiesAsync()
        {
            var properties = await _propertyRepository.GetAllListAsync();
            return ObjectMapper.Map<List<PropertyDto>>(properties);
        }

        public async Task<PropertyDto> GetPropertyAsync(Guid id)
        {
            var property = await _propertyRepository.GetAsync(id);
            return ObjectMapper.Map<PropertyDto>(property);
        }

        public async Task<UpdatePropertyDto> UpdatePropertyAsync(UpdatePropertyDto propertyDto)
        {
            var property = await _propertyRepository.GetAsync(propertyDto.Id);
            ObjectMapper.Map(propertyDto, property);
            await _propertyRepository.UpdateAsync(property);

            return ObjectMapper.Map<UpdatePropertyDto>(property);
        }

    }
}
