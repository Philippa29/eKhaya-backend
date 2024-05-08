using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Images;
using eKhaya.Domain.Properties;
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
        const string BASE_IMAGE_PATH = "App_Data/Images";
        private readonly IRepository<UnitsAmenities, Guid> _unitsAmenitiesRepository;
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IRepository<Amenity, Guid> _amenitiesRepository;
        private readonly IRepository<Property, Guid> _propertyRepository;
        private readonly IRepository<Image, Guid> _imageRepository;


        public UnitsAmenitiesAppService(IRepository<UnitsAmenities, Guid> unitsAmenitiesRepository, IRepository<Unit, Guid> unitRepository, IRepository<Amenity, Guid> amenitiesRepository, IRepository<Property, Guid> propertyRepository, IRepository<Image, Guid> imageRepository)
        {
            _unitsAmenitiesRepository = unitsAmenitiesRepository;
            _unitRepository = unitRepository;
            _amenitiesRepository = amenitiesRepository;
            _propertyRepository = propertyRepository;
            _imageRepository = imageRepository;
        }

        public async Task<List<UnitsAmenitiesDto>> CreateUnitsAmenitiesAsync(UnitsAmenitiesDto input)
        {
            // Retrieve the property
            var property = await _propertyRepository.GetAsync(input.Property) ?? throw new Exception("Property not found");

            var createdUnitAmenities = new List<UnitsAmenitiesDto>();

            // Iterate over each Amenity GUID
            foreach (var amenityId in input.Amenity)
            {
                // Retrieve the amenity
                var amenity = await _amenitiesRepository.GetAsync(amenityId) ?? throw new Exception($"Amenity with ID {amenityId} not found");

                // Create the unit amenity entity
                var unitAmenity = new UnitsAmenities
                {
                    Property = property,
                    UnitType = input.UnitType,
                    Amenity = amenity
                };

                // Insert the unit amenity entity into the repository
                var createdUnitAmenityEntity = await _unitsAmenitiesRepository.InsertAsync(unitAmenity);

                // Map the created unit amenity entity to the DTO and add it to the list
                var createdUnitAmenityDto = new UnitsAmenitiesDto
                {
                    Id = createdUnitAmenityEntity.Id,
                    Property = createdUnitAmenityEntity.Property.Id,
                    UnitType = createdUnitAmenityEntity.UnitType,
                    Amenity = new List<Guid> { amenityId } // Assign the amenityId within a list
                };


                createdUnitAmenities.Add(createdUnitAmenityDto);
            }

            // Save changes to the database
            await CurrentUnitOfWork.SaveChangesAsync();

            // Return the list of created unit amenity DTOs
            return createdUnitAmenities;
        }

        public async Task DeleteUnitsAmenitiesAsync(Guid id)
        {
            await _unitsAmenitiesRepository.DeleteAsync(id);
        }

        public async Task<List<UnitsAmenitiesDto>> GetAllUnitsAmenitiesAsync()
        {
            var unitsAmenities = await _unitsAmenitiesRepository.GetAllIncluding(x => x.Amenity , y => y.Property).ToListAsync();

            return ObjectMapper.Map<List<UnitsAmenitiesDto>>(unitsAmenities);
        }

        public async Task<List<UnitsAmenitiesDto>> GetUnitsAmenitiesForUnitAsync(Guid id)
        {
            var unitsAmenities = await _unitsAmenitiesRepository.GetAllIncluding(x => x.Amenity, y => y.Property).Where(x => x.Property.Id == id).ToListAsync();

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


        public async Task<List<ViewUnitsPerPropertyDto>> GetUnitDetailsForProperty(Guid propertyId)
        {
            // Check if the property exists
            var property = await _propertyRepository.GetAsync(propertyId);
            if (property == null)
            {
                throw new Exception("Property not found");
            }

            var unitDetails = new List<ViewUnitsPerPropertyDto>();

            // Iterate through each unit type to fetch details
            foreach (UnitType unitType in Enum.GetValues(typeof(UnitType)))
            {
                // Get units of the current type associated with the property
                

                // Iterate through each unit of the current type
              
                    // Get amenities for the unit type
                    var amenities = await _unitsAmenitiesRepository.GetAll()
                        .Where(ua => ua.Property.Id == propertyId && ua.UnitType == unitType)
                        .Include(ua => ua.Amenity) // Include the Amenity navigation property
                        .Select(ua => ua.Amenity.Name)
                        .ToListAsync();

                    // Get images for the property, unit, and unit type
                    var images = await _imageRepository.GetAll()
                        .Where(img => img.OwnerID == propertyId && img.ImageType == GetImageTypeByUnitType(unitType) )
                        .ToListAsync();

                    // Convert images to base64
                    var base64Images = images.Select(img =>
                    {
                        var imagePath = $"{BASE_IMAGE_PATH}/{img.ImageName}";
                        var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                        return Convert.ToBase64String(imageBytes);
                    }).ToList();

                    // Create DTO with unit details
                    var unitDto = new ViewUnitsPerPropertyDto
                    {
                        PropertyId = propertyId,
                        UnitType = unitType,
                        Amenities = amenities,
                        Base64Images = base64Images
                    };

                    unitDetails.Add(unitDto);
                
            }

            return unitDetails;
        }



        private ImageType GetImageTypeByUnitType(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Bachelor:
                    return ImageType.Unit_Bachelor;
                case UnitType.OneBedroom:
                    return ImageType.Unit_1Bedroom;
                case UnitType.TwoBedroom:
                    return ImageType.Unit_2Bedroom;
                default:
                    throw new ArgumentException("Invalid unit type");
            }
        }






    }
}
