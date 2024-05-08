using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ResidentService
{
    public interface IResidentAppService : IApplicationService
    {
        Task<ResidentDto> CreateResidentAsync(CreateResidentDto input);

        Task<ResidentDto> UpdateResidentAsync(UpdateResidentDto input);

        Task<ResidentDto> GetResidentAsync(Guid id);

        Task<List<ResidentDto>> GetAllResidentsAsync();

        Task DeleteResidentAsync(Guid id);
    }
}
