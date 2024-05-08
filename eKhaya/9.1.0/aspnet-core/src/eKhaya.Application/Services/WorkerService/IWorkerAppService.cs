using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.WorkerService
{
    public interface  IWorkerAppService : IApplicationService
    {
        public Task<WorkerDto> CreateWorkerAsync(CreateWorkerDto input);

        public Task<WorkerDto> UpdateWorkerAsync(WorkerDto input);

        public Task<WorkerDto> GetWorkerAsync(Guid id);

        public Task<List<WorkerDto>> GetAllWorkersAsync();

        public Task DeleteWorkerAsync(Guid id);
    }
}
