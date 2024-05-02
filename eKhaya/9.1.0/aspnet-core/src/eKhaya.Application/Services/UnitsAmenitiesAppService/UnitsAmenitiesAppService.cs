using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.Units;
using eKhaya.Domain.UnitsAmenities;
using eKhaya.Services.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.UnitsAmenitiesAppService
{
    public class UnitsAmenitiesAppService : ApplicationService, IUnitsAmenitiesAppService
    {
        private readonly IRepository<UnitsAmenities, Guid> _unitsAmenitiesRepository;
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IRepository<Amenity, Guid> _amenitiesRepository;

        public UnitsAmenitiesAppService(IRepository<UnitsAmenities, Guid> unitsAmenitiesRepository, IRepository<Unit, Guid> unitRepository, IRepository<Amenity, Guid> amenitiesRepository)
        {
            _unitsAmenitiesRepository = unitsAmenitiesRepository;
            _unitRepository = unitRepository;
            _amenitiesRepository = amenitiesRepository;
        }

        public async Task<UnitsAmenitiesDto> CreateUnitsAmenitiesAsync(UnitsAmenitiesDto input)
        {
            var unit = await _unitRepository.GetAsync(input.UnitId) ?? throw new Exception("Unit not found");

            var amenity = await _amenitiesRepository.GetAsync(input.AmenityId) ?? throw new Exception("Amenity not found");

            var unitAmenity = new UnitsAmenities
            {
               Unit = unit,
               Amenity = amenity
            };

            var createdUnitAmenity = await _unitsAmenitiesRepository.InsertAsync(unitAmenity);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<UnitsAmenitiesDto>(createdUnitAmenity);
        }

        public async Task DeleteUnitsAmenitiesAsync(Guid id)
        {
            await _unitsAmenitiesRepository.DeleteAsync(id);
        }

        public async Task<List<UnitsAmenitiesDto>> GetAllUnitsAmenitiesAsync()
        {
            var unitsAmenities = await _unitsAmenitiesRepository.GetAllIncluding(x => x.Amenity , y => y.Unit).ToListAsync();

            return ObjectMapper.Map<List<UnitsAmenitiesDto>>(unitsAmenities);
        }

        public async Task<List<UnitsAmenitiesDto>> GetUnitsAmenitiesForUnitAsync(Guid id)
        {
            var unitsAmenities = await _unitsAmenitiesRepository.GetAllIncluding(x => x.Amenity, y => y.Unit).Where(x => x.Unit.Id == id).ToListAsync();

            return ObjectMapper.Map<List<UnitsAmenitiesDto>>(unitsAmenities);
        }


        public async Task<UnitsAmenitiesDto> UpdateUnitsAmenitiesAsync(UnitsAmenitiesDto input)
        {
            // Retrieve the existing unit amenity entity by its ID
            var unitAmenity = await _unitsAmenitiesRepository.GetAsync(input.Id);

            // Map the input DTO properties to the entity
            ObjectMapper.Map(input, unitAmenity);

            // Update the entity in the repository
            var updatedUnitAmenity = await _unitsAmenitiesRepository.UpdateAsync(unitAmenity);

            // Map the updated entity back to a DTO and return it
            return ObjectMapper.Map<UnitsAmenitiesDto>(updatedUnitAmenity);
        }




    }
}
