using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eKhaya.Services.LeaseAppService
{
    public interface ILeaseAppService
    {
        Task<ReturnLeaseDto> CreateLeaseAsync(LeaseDto input);

        Task<List<ReturnLeaseDto>> GetAllLeasesAsync(Guid ownerId);

        Task<LeaseDto> GetLeaseAsync(Guid id);

        Task UpdateLeaseAsync(Guid id, LeaseDto input);

        Task DeleteLeaseAsync(Guid id);
    }
}
