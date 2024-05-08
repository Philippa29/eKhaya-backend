using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.AgentsProperty;
using Microsoft.EntityFrameworkCore;

using eKhaya.Domain.Amenities;
using eKhaya.Domain.Properties;
using eKhaya.Domain.PropertyAmenities;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.PropertyAgentsService
{
    public class PropertyAgentAppService : ApplicationService, IPropertyAgentAppService
    {
        private readonly IRepository<AgentProperty, Guid> _propertyagentRepository;
        private readonly IRepository<Property, Guid> _propertyRepository;
        private readonly IRepository<Agent, Guid> _agentRepository;

        public PropertyAgentAppService(IRepository<AgentProperty, Guid> propertyagentRepository, IRepository<Property, Guid> propertyRepository, IRepository<Agent, Guid> agentRepository)
        {
            _propertyagentRepository = propertyagentRepository;
            _propertyRepository = propertyRepository;
            _agentRepository = agentRepository;
        }

        public async Task<PropertyAgentsDto> CreatePropertyAgentAsync(PropertyAgentsDto input)
        {
            var property = await _propertyRepository.GetAsync(input.Property) ?? throw new Exception("Property not found");

            var agent = await _agentRepository.GetAsync(input.Agent) ?? throw new Exception("Agent not found");

            var PropertyAgent = new AgentProperty
            {
                Property = property,
                Agent = agent
            };


            var createdPropertyAgent = await _propertyagentRepository.InsertAsync(PropertyAgent);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<PropertyAgentsDto>(createdPropertyAgent);
        }

        public async Task DeletePropertyAgentAsync(Guid id)
        {
            await _propertyagentRepository.DeleteAsync(id);
        }

        public async Task<PropertyAgentsDto> GetPropertyAgentAsync(Guid id )
        {
            var PropertyAgent = await _propertyagentRepository.GetAsync(id);

            return ObjectMapper.Map<PropertyAgentsDto>(PropertyAgent);
        }

        public async Task<List<PropertyAgentsDto>> GetAllAgentsPerProperty(Guid id)
        {
            var PropertyAgent = await _propertyagentRepository.GetAllIncluding(x => x.Agent, y => y.Property).Where(x => x.Property.Id == id).ToListAsync();

            return ObjectMapper.Map<List<PropertyAgentsDto>>(PropertyAgent);
        }


        public async Task<PropertyAgentsDto> UpdatePropertyAgentAsync(PropertyAgentsDto input)
        {
            // Retrieve the existing unit amenity entity by its ID
            var PropertyAgent = await _propertyagentRepository.GetAsync(input.Id);

            // Map the input DTO properties to the entity
            ObjectMapper.Map(input, PropertyAgent);

            // Update the entity in the repository
            var updatedUnitAmenity = await _propertyagentRepository.UpdateAsync(PropertyAgent);

            // Map the updated entity back to a DTO and return it
            return ObjectMapper.Map<PropertyAgentsDto>(updatedUnitAmenity);
        }
    }
}

