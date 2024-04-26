using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.UnitsService
{
    public interface IUnitsAppService : IApplicationService
    {
        Task<UnitsDto> CreateUnitsAsync(UnitsDto input);

        Task<UpdateUnitsDto> UpdateUnitsAsync(UpdateUnitsDto input);

        Task<UnitsDto> GetUnitsAsync(Guid id);

        Task<List<UnitsDto>> GetAllUnitsAsync();

        Task DeleteUnitsAsync(Guid id);
    }
}
