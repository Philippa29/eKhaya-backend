using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.Amenities;
using eKhaya.Services.Dtos;
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
            var amenity = ObjectMapper.Map<Amenity>(input);
            await _amenitiesRepository.InsertAsync(amenity);
            CurrentUnitOfWork.SaveChanges();
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

        public async Task<AmenitiesDto> UpdateAmenitiesAsync(AmenitiesDto input)
        {
            var amenity = _amenitiesRepository.GetAsync(input.Id);

            ObjectMapper.Map(input, amenity);

            return ObjectMapper.Map<AmenitiesDto>(amenity);

        }
    }
}
