using Abp.Application.Services;
using eKhaya.Domain.Amenities;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.PropertyService
{
    public interface IPropertyAppService : IApplicationService
    {
        Task <PropertyDto> CreatePropertyAsync(PropertyDto propertyDto); 

        Task<PropertyDto> UpdatePropertyAsync(UpdatePropertyDto input);

        Task<PropertyDto> GetPropertyAsync(Guid id);

        Task<List<PropertyDto>> GetAllPropertiesAsync();

        Task DeletePropertyAsync(Guid id);
    }
}
