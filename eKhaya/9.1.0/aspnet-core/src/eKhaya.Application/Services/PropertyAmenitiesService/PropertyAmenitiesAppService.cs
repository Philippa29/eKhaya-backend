using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.Properties;
using eKhaya.Domain.PropertyAmenities;
using Microsoft.EntityFrameworkCore;

using eKhaya.Domain.Units;
using eKhaya.Domain.UnitsAmenities;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eKhaya.Domain.Images;
using eKhaya.Domain.ENums;

namespace eKhaya.Services.PropertyAmenitiesService
{
    public class PropertyAmenitiesAppService : ApplicationService , IPropertyAmenitiesAppService
    {
        const string BASE_IMAGE_PATH = "App_Data/Images";
        private readonly IRepository<PropertyAmenity, Guid> _propertyAmenitiesRepository;
        private readonly IRepository<Property ,  Guid> _propertyRepository;
        private readonly IRepository<Amenity, Guid> _amenitiesRepository;
        private readonly IRepository<Image, Guid> _imageRepository;
        private readonly IRepository<Unit, Guid> _unitRepository;

        public PropertyAmenitiesAppService(IRepository<PropertyAmenity, Guid> propertyAmenitiesRepository, IRepository<Property, Guid> propertyRepository, IRepository<Amenity, Guid> amenitiesRepository, IRepository<Image, Guid> imageRepository, IRepository<Unit, Guid> unitRepository)
        {
            _propertyAmenitiesRepository = propertyAmenitiesRepository;
            _propertyRepository = propertyRepository;
            _amenitiesRepository = amenitiesRepository;
            _imageRepository = imageRepository;
            _unitRepository = unitRepository;
        }

        public async Task<PropertyAmenitiesDto> CreatePropertyAmenitiesAsync(PropertyAmenitiesDto input)
        {
            var property = await _propertyRepository.GetAsync(input.Property) ?? throw new Exception("Property not found");

            var amenity = await _amenitiesRepository.GetAsync(input.Amenities) ?? throw new Exception("Amenity not found");

            var unitAmenity = new PropertyAmenity
            {
                Property = property,
                Amenity = amenity
            };
            

            var createdUnitAmenity = await _propertyAmenitiesRepository.InsertAsync(unitAmenity);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<PropertyAmenitiesDto>(createdUnitAmenity);
        }

        public async Task DeletePropertyAmenitiesAsync(Guid id)
        {
            await _propertyAmenitiesRepository.DeleteAsync(id);
        }

        public async Task<List<PropertyAmenitiesDto>> GetAllPropertyAmenitiesAsync()
        {
            var propertyAmenities = await _propertyAmenitiesRepository.GetAllIncluding(x => x.Amenity, y => y.Property).ToListAsync();

            return ObjectMapper.Map<List<PropertyAmenitiesDto>>(propertyAmenities);
        }

        public async Task<List<PropertyAmenitiesDto>> GetPropertyAmenitiesForUnitAsync(Guid id)
        {
            var propertyAmenities = await _propertyAmenitiesRepository.GetAllIncluding(x => x.Amenity, y => y.Property).Where(x => x.Property.Id == id).ToListAsync();

            return ObjectMapper.Map<List<PropertyAmenitiesDto>>(propertyAmenities);
        }


        public async Task<PropertyAmenitiesDto> UpdatePropertyAmenitiesAsync(PropertyAmenitiesDto input)
        {
            // Retrieve the existing unit amenity entity by its ID
            var propertyAmenity = await _propertyAmenitiesRepository.GetAsync(input.Id);

            // Map the input DTO properties to the entity
            ObjectMapper.Map(input, propertyAmenity);

            // Update the entity in the repository
            var updatedUnitAmenity = await _propertyAmenitiesRepository.UpdateAsync(propertyAmenity);

            // Map the updated entity back to a DTO and return it
            return ObjectMapper.Map<PropertyAmenitiesDto>(updatedUnitAmenity);
        }

        public async Task<List<ViewPropertyDto>> GetAllAvailableProperties()
        {
            // Retrieve all available units
            var availableUnits = await _unitRepository.GetAll()
                .Where(unit => unit.Availability)
                .Include(unit => unit.PropertyID)
                .ToListAsync();

            var properties = new List<ViewPropertyDto>();

            foreach (var unit in availableUnits)
            {
                var propertyId = unit.PropertyID.Id;

                // Retrieve amenities for the property
                var amenities = await _propertyAmenitiesRepository.GetAll()
                    .Where(pa => pa.Property.Id == propertyId)
                    .Select(pa => pa.Amenity.Name)
                    .ToListAsync();

                // Retrieve image for the property
                var image = await _imageRepository.FirstOrDefaultAsync(image => image.OwnerID == propertyId);

                string base64Image = null;
                if (image != null)
                {
                    var imagePath = $"{BASE_IMAGE_PATH}/{image.ImageName}";
                    var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                    base64Image = Convert.ToBase64String(imageBytes);
                }

                // Create ViewPropertyDto
                var propertyDto = new ViewPropertyDto
                {
                    PropertyId = propertyId,
                    PropertyName = unit.PropertyID.PropertyName,
                    Description = unit.PropertyID.Description,
                    Amenities = amenities,
                    Base64Image = base64Image
                };

                properties.Add(propertyDto);
            }

            return properties;
        }








    }
}
