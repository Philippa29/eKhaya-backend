using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.Amenities;
using eKhaya.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.AmenitiesService
{
    public class AmenitiesAppService : ApplicationService , IAmenitiesAppService
    {
        private readonly IRepository<Amenity, Guid> _amenitiesRepository;

        public AmenitiesAppService(IRepository<Amenity, Guid> amenitiesRepository)
        {
            _amenitiesRepository = amenitiesRepository;
        }
        public async Task<AmenitiesDto> CreateAmenitiesAsync(AmenitiesDto input)
        {
            // Check if an amenity with the same Name and Type already exists
            var existingAmenity = await _amenitiesRepository.FirstOrDefaultAsync(x =>
                x.Name == input.Name && x.Type == input.Type);

            if (existingAmenity != null)
            {
                // If an amenity with similar details exists, throw an error
                throw new Exception("An amenity with the same name and type already exists.");
            }

            // If no similar amenity exists, proceed with creating the new amenity
            var amenity = ObjectMapper.Map<Amenity>(input);
            await _amenitiesRepository.InsertAsync(amenity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<AmenitiesDto>(amenity);
        }


        public async Task DeleteAmenitiesAsync(Guid id)
        {
            await _amenitiesRepository.DeleteAsync(id);
        }

        public async Task<List<AmenitiesDto>> GetAllAmenitiesAsync()
        {
            var amenities = await _amenitiesRepository.GetAllListAsync();

            return ObjectMapper.Map<List<AmenitiesDto>>(amenities);
        }

        public async Task<AmenitiesDto> GetAmenitiesAsync(Guid id)
        {
           var amenity = await _amenitiesRepository.GetAsync(id);
            return ObjectMapper.Map<AmenitiesDto>(amenity); 
        }

        [HttpPut("updateAmenities/{id}")]

        public async Task<AmenitiesDto> UpdateAmenitiesAsync(AmenitiesDto input)
        {
            var amenity = await _amenitiesRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, amenity);
            var updatedAmenity = await _amenitiesRepository.UpdateAsync(amenity);

            return ObjectMapper.Map<AmenitiesDto>(updatedAmenity);

        }
    }
}
