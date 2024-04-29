using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.MaintenanceRequestService
{
     public interface IMaintenanceRequestAppService : IApplicationService
    {
        public Task<MaintenanceRequestDto> CreateMaintenanceRequestAsync(CreateMaintenanceRequestDto input);

        public Task<MaintenanceRequestDto> UpdateMaintenanceRequestAsync(MaintenanceRequestDto input);

        public Task<MaintenanceRequestDto> GetMaintenanceRequestAsync(Guid id);

        public Task<List<MaintenanceRequestDto>> GetAllMaintenanceRequestsAsync();

        public Task DeleteMaintenanceRequestAsync(Guid id);
    }
}
