using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.UnitsAmenitiesAppService
{
    public interface IUnitsAmenitiesAppService : IApplicationService 
    {
        public Task<UnitsAmenitiesDto> CreateUnitsAmenitiesAsync(UnitsAmenitiesDto input);

        public Task<UnitsAmenitiesDto> UpdateUnitsAmenitiesAsync(UnitsAmenitiesDto input);

        public Task<List<UnitsAmenitiesDto>> GetUnitsAmenitiesForUnitAsync(Guid id);

        public Task<List<UnitsAmenitiesDto>> GetAllUnitsAmenitiesAsync();

        public Task DeleteUnitsAmenitiesAsync(Guid id);
    }
}
