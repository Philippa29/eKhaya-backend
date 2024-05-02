using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using eKhaya.Domain.MaintenanceRequests;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.MaintenanceRequestService
{
    public class MaintenanceRequestAppService : IApplicationService,  IMaintenanceRequestAppService
    {
        private readonly IRepository<MaintenanceRequest, Guid> _maintenancerequestrepository;
        private readonly IRepository<Resident, Guid> _residentrepository;
        
        private readonly IObjectMapper _objectMapper;


        public MaintenanceRequestAppService(IRepository<MaintenanceRequest, Guid> maintenancerequestrepository, IRepository<Resident, Guid> residentrepository, IObjectMapper objectMapper)
        {
            _maintenancerequestrepository = maintenancerequestrepository;
            _residentrepository = residentrepository;
            _objectMapper = objectMapper;
        }

        public async Task<MaintenanceRequestDto> CreateMaintenanceRequestAsync(CreateMaintenanceRequestDto input)
        {
            var maintenancerequest = _objectMapper.Map<MaintenanceRequest>(input);
            await _maintenancerequestrepository.InsertAsync(maintenancerequest);

            return _objectMapper.Map<MaintenanceRequestDto>(maintenancerequest);
            
        }

        public async Task DeleteMaintenanceRequestAsync(Guid id)
        {
            await _maintenancerequestrepository.DeleteAsync(id);
        }

        public async Task<List<MaintenanceRequestDto>> GetAllMaintenanceRequestsAsync()
        {
            var maintenancerequests = await _maintenancerequestrepository
                .GetAllIncluding(x => x.UnitID, y => y.Tenant, w => w.Worker)
                .ToListAsync();

            return _objectMapper.Map<List<MaintenanceRequestDto>>(maintenancerequests);
        }

        public async Task<MaintenanceRequestDto> GetMaintenanceRequestAsync(Guid id)
        {
            var maintenancerequest = await _maintenancerequestrepository.GetAsync(id);
            return _objectMapper.Map<MaintenanceRequestDto>(maintenancerequest);
        }

        public async Task<MaintenanceRequestDto> UpdateMaintenanceRequestAsync(MaintenanceRequestDto input)
        {
            var maintenancerequest = await _maintenancerequestrepository.GetAsync(input.Id);
            _objectMapper.Map(input, maintenancerequest);
            await _maintenancerequestrepository.UpdateAsync(maintenancerequest);
            return _objectMapper.Map<MaintenanceRequestDto>(maintenancerequest);
        }

        public async Task<List<MaintenanceRequestDto>> GetMaintenanceRequestsByResident(Guid id)
        {
            var resident = await _residentrepository.GetAsync(id);
            var maintenancerequests = await _maintenancerequestrepository.GetAll().Where(request => request.Tenant == resident).ToListAsync();

            return _objectMapper.Map<List<MaintenanceRequestDto>>(maintenancerequests);

        }

    }
}
