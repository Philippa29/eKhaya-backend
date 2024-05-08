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
using eKhaya.Sessions;
using Microsoft.AspNetCore.Http;

namespace eKhaya.Services.MaintenanceRequestService
{
    public class MaintenanceRequestAppService : IApplicationService
    {
        private readonly IRepository<MaintenanceRequest, Guid> _maintenanceRequestRepository;
        private readonly IRepository<Resident, Guid> _residentRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly ISessionAppService _session;

        public MaintenanceRequestAppService(IRepository<MaintenanceRequest, Guid> maintenanceRequestRepository, IRepository<Resident, Guid> residentRepository, IObjectMapper objectMapper, ISessionAppService session)
        {
            _maintenanceRequestRepository = maintenanceRequestRepository;
            _residentRepository = residentRepository;
            _objectMapper = objectMapper;
            _session = session;
        }

        public async Task<MaintenanceRequestDto> CreateMaintenanceRequestAsync(CreateMaintenanceRequestDto input)
        {
            // Retrieve the logged-in user's ID from the session
            var loggedInUserId = _session.GetCurrentLoginInformations();

            // Retrieve the resident associated with the logged-in user
            var resident = await _residentRepository.FirstOrDefaultAsync(r => r.User.Id == loggedInUserId.Id);

            if (resident == null)
            {
                throw new ApplicationException("Resident not found for logged-in user.");
            }

            // Create a new MaintenanceRequest entity using the provided input and resident information
            var maintenanceRequest = new MaintenanceRequest
            {
                Type = input.Type,
                Status = input.Status,
                CreatedDate = DateTime.UtcNow,
                DateCompleted = input.DateCompleted,
                Tenant = resident,
                UnitID = input.UnitID
            };

            // Insert the new maintenance request into the repository
            await _maintenanceRequestRepository.InsertAsync(maintenanceRequest);

            // Create and return a new MaintenanceRequestDto from the created entity
            var maintenanceRequestDto = new MaintenanceRequestDto
            {
                Id = maintenanceRequest.Id,
                Type = maintenanceRequest.Type,
                Status = maintenanceRequest.Status,
                CreatedDate = maintenanceRequest.CreatedDate,
                DateCompleted = maintenanceRequest.DateCompleted,
                Tenant = maintenanceRequest.Tenant.Id,
                UnitID = maintenanceRequest.UnitID
            };

            return maintenanceRequestDto;
        }

        public async Task DeleteMaintenanceRequestAsync(Guid id)
        {
            await _maintenanceRequestRepository.DeleteAsync(id);
        }

        public async Task<List<MaintenanceRequestDto>> GetAllMaintenanceRequestsAsync()
        {
            var maintenancerequests = await _maintenanceRequestRepository
                .GetAllIncluding(x => x.UnitID, y => y.Tenant)
                .ToListAsync();

            return _objectMapper.Map<List<MaintenanceRequestDto>>(maintenancerequests);
        }

        public async Task<MaintenanceRequestDto> GetMaintenanceRequestAsync(Guid id)
        {
            var maintenancerequest = await _maintenanceRequestRepository.GetAsync(id);
            return _objectMapper.Map<MaintenanceRequestDto>(maintenancerequest);
        }

        public async Task<MaintenanceRequestDto> UpdateMaintenanceRequestAsync(MaintenanceRequestDto input)
        {
            var maintenancerequest = await _maintenanceRequestRepository.GetAsync(input.Id);
            _objectMapper.Map(input, maintenancerequest);
            await _maintenanceRequestRepository.UpdateAsync(maintenancerequest);
            return _objectMapper.Map<MaintenanceRequestDto>(maintenancerequest);
        }

        public async Task<List<MaintenanceRequestDto>> GetMaintenanceRequestsByResident(Guid id)
        {
            var resident = await _residentRepository.GetAsync(id);
            var maintenancerequests = await _maintenanceRequestRepository.GetAll().Where(request => request.Tenant == resident).ToListAsync();

            return _objectMapper.Map<List<MaintenanceRequestDto>>(maintenancerequests);

        }

    }
}
