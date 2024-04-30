using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.PropertyAmenitiesService
{
    public interface IPropertyAmenitiesAppService
    {
        public Task<PropertyAmenitiesDto> CreatePropertyAmenitiesAsync(PropertyAmenitiesDto input);

        public Task<PropertyAmenitiesDto> UpdatePropertyAmenitiesAsync(PropertyAmenitiesDto input);

        public Task<List<PropertyAmenitiesDto>> GetPropertyAmenitiesForUnitAsync(Guid id);

        public Task<List<PropertyAmenitiesDto>> GetAllPropertyAmenitiesAsync();

        public Task DeletePropertyAmenitiesAsync(Guid id);

        public Task<List<ViewPropertyDto>> GetAllAvailableProperties(); 
    }
}
