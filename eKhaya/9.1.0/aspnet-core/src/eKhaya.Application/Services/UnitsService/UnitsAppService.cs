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
using Abp.Domain.Uow;
using eKhaya.Sessions;

namespace eKhaya.Services.UnitsService
{
    public class UnitsAppService : ApplicationService,  IUnitsAppService
    {
        private readonly IRepository<Unit, Guid> _unitsRepository;
        
        private readonly IRepository<Property, Guid> _propertyRepository;
        private readonly IRepository<Agent, Guid> _agentRepository;
        private readonly IRepository<UnitsAmenities, Guid> _unitsAmenitiesRepository;
        private readonly IRepository<Amenity, Guid> _amenityRepository;
        private readonly ISessionAppService _session;

        public UnitsAppService(IRepository<Unit, Guid> unitsRepository, IRepository<Property, Guid> propertyRepository, IRepository<Agent, Guid> agentRepository, IRepository<UnitsAmenities, Guid> unitsAmenitiesRepository, IRepository<Amenity, Guid> amenityRepository, ISessionAppService session)
        {
            _unitsRepository = unitsRepository;
            _propertyRepository = propertyRepository;
            _agentRepository = agentRepository;
            _unitsAmenitiesRepository = unitsAmenitiesRepository;
            
            _amenityRepository = amenityRepository;
            _session = session;
        }

        public async Task<List<UnitsDto>> CreateUnitsAsync(CreateUnitsDto input)
        {
            var agent = await _agentRepository.GetAsync(input.AgentID) ?? throw new Exception("Agent not found");
            var property = await _propertyRepository.GetAsync(input.PropertyID) ?? throw new Exception("Property not found");

            // Check if any units with the specified property, type, and level already exist
            var existingUnits = await _unitsRepository
                .GetAll()
                .Where(u => u.PropertyID.Id == input.PropertyID && u.Type == input.Type && u.Level == input.Level)
                .ToListAsync();

            if (existingUnits.Any())
            {
                throw new Exception("Units already exist for the given property, type, and level combination");
            }


            if (existingUnits.Any())
            {
                throw new Exception("Unit number range already exists for the given property and type");
            }

            // Create units based on the range of unit numbers
            var createdUnits = new List<Unit>();
            for (int i = input.RangeLower; i <= input.RangeUpper; i++)
            {
                var unit = new Unit
                {
                    Size = input.Size,
                    Type = input.Type,
                    UnitNumber = i.ToString(), // Convert int to string for unit number
                    Level = input.Level,
                    Availability = input.Availability,
                    AgentID = agent,
                    PropertyID = property
                };

                createdUnits.Add(await _unitsRepository.InsertAsync(unit));
            }

            // Return the created units
            return ObjectMapper.Map<List<UnitsDto>>(createdUnits);
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


        public async Task<List<UnitsDto>> GetAllAvailableUnitsByProperty(Guid propertyId)
        {

            // Query available units by property ID
            var availableUnits = await _unitsRepository
                .GetAllIncluding(u => u.AgentID, u => u.PropertyID)
                .Where(u => u.PropertyID.Id == propertyId && u.Availability == true)
                .ToListAsync();

            // Map units to DTOs
            var unitsDto = ObjectMapper.Map<List<UnitsDto>>(availableUnits);

            return unitsDto;
        }



        public async Task<List<UnitsDto>> GetAllAvailableUnitsForAgent()
        {
            var loginUser = await _session.GetCurrentLoginInformations();

            if (loginUser.User == null)
            {
                throw new Exception("User not found");
            }

            var agent = await _agentRepository.FirstOrDefaultAsync(a => a.User.UserName == loginUser.User.UserName);

            if (agent == null)
            {
                throw new Exception("Agent not found for the logged-in user");
            }

            // Query available units by property ID for the specific agent
            var availableUnits = await _unitsRepository
                .GetAllIncluding(u => u.AgentID, u => u.PropertyID)
                .Where(u => u.AgentID.Id == agent.Id && u.Availability == true)
                .ToListAsync();

            // Map units to DTOs
            var unitsDto = ObjectMapper.Map<List<UnitsDto>>(availableUnits);

            return unitsDto;
        }


        public async Task<List<UnitsDto>> GetAllUnitsAsync()
        {
            var units = await _unitsRepository.GetAllIncluding(x => x.AgentID, x => x.PropertyID).ToListAsync();
            return ObjectMapper.Map<List<UnitsDto>>(units);
        }

        public async Task<UpdateUnitsDto> UpdateUnitsAsync(UpdateUnitsDto input)
        {

            var loginUser = await _session.GetCurrentLoginInformations();

            if (loginUser.User == null)
            {
                throw new Exception("User not found");
            }

            var agent = await _agentRepository.FirstOrDefaultAsync(x => x.User.UserName == loginUser.User.UserName);
            if (agent == null)
            {
                throw new Exception("Agent not found");
            }
           var unit = await _unitsRepository.GetAsync(input.Id) ?? throw new Exception("Unit not found");

            unit.Size = input.Size;
            unit.Type = input.Type;
            unit.UnitNumber = input.UnitNumber;
            unit.Level = input.Level;
            unit.Availability = input.Availability;
            unit.AgentID = agent; 

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
