using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.Properties;
using eKhaya.Domain.Units;
using eKhaya.Domain.UnitsAmenities;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.UnitsService
{
    public class UnitsAppService : ApplicationService,  IUnitsAppService
    {
        private readonly IRepository<Unit, Guid> _unitsRepository;
        private readonly IRepository<Property, Guid> _propertyRepository;
        private readonly IRepository<Agent, Guid> _agentRepository;
        private readonly IRepository<UnitsAmenities, Guid> _unitsAmenitiesRepository;
        private readonly IRepository<Amenity, Guid> _amenityRepository;

        public UnitsAppService(IRepository<Unit, Guid> unitsRepository, IRepository<Property, Guid> propertyRepository, IRepository<Agent, Guid> agentRepository, IRepository<UnitsAmenities, Guid> unitsAmenitiesRepository, IRepository<Amenity, Guid> amenityRepository)
        {
            _unitsRepository = unitsRepository;
            _propertyRepository = propertyRepository;
            _agentRepository = agentRepository;
            _unitsAmenitiesRepository = unitsAmenitiesRepository;
            _amenityRepository = amenityRepository;
        }

        public async Task<UnitsDto> CreateUnitsAsync(UnitsDto input)
        {
            var agent = await _agentRepository.GetAsync(input.AgentID) ?? throw new Exception("Agent not found");

            var property = await _propertyRepository.GetAsync(input.PropertyID) ?? throw new Exception("Property not found");
            
            var unit = new Unit
            {
                Size = input.Size,
                Type = input.Type,
                UnitNumber = input.UnitNumber,
                Level = input.Level,
                Availability = input.Availability,
            };

            unit.AgentID = agent;
            unit.PropertyID = property;

            var createdUnit = await _unitsRepository.InsertAsync(unit);
            //save changes 

            await CurrentUnitOfWork.SaveChangesAsync();

            foreach (var amenityId in input.AmenityIds)
            {
                var amenity = await _amenityRepository.GetAsync(amenityId) ?? throw new Exception("Property not found");
                var unitsAmenities = new UnitsAmenities
                {
                    Unit = createdUnit,
                    Amenity = amenity,

                };

                await _unitsAmenitiesRepository.InsertAsync(unitsAmenities);
            }

            return ObjectMapper.Map<UnitsDto>(createdUnit);
        }

        public async Task DeleteUnitsAsync(Guid id)
        {
            await _unitsRepository.DeleteAsync(id);
        }

        public async Task<UnitsDto> GetUnitsAsync(Guid id)
        {
            
                var unit = await _unitsRepository
                    .GetAllIncluding(u => u.AgentID, u => u.PropertyID)
                    .FirstOrDefaultAsync(u => u.Id == id);

                return ObjectMapper.Map<UnitsDto>(unit);
            

        }


        public async Task<List<UnitsDto>> GetAllUnitsAsync()
        {
            var units = await _unitsRepository.GetAllIncluding(x => x.AgentID, x => x.PropertyID).ToListAsync();
            return ObjectMapper.Map<List<UnitsDto>>(units);
        }

        public async Task<UpdateUnitsDto> UpdateUnitsAsync(UpdateUnitsDto input)
        {
           var unit = await _unitsRepository.GetAsync(input.Id) ?? throw new Exception("Unit not found");

            unit.Size = input.Size;
            unit.Type = input.Type;
            unit.UnitNumber = input.UnitNumber;
            unit.Level = input.Level;
            unit.Availability = input.Availability;
            unit.AgentID = await _agentRepository.GetAsync(input.AgentID) ?? throw new Exception("Agent not found");

            var updatedUnit = await _unitsRepository.UpdateAsync(unit);


            var update = new UpdateUnitsDto
            {
                Id = updatedUnit.Id,
                Size = updatedUnit.Size,
                Type = updatedUnit.Type,
                UnitNumber = updatedUnit.UnitNumber,
                Level = updatedUnit.Level,
                Availability = updatedUnit.Availability,
                AgentID = updatedUnit.AgentID.Id
            }; 
            return update;
        }


    }
    
}
