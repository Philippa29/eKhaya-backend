using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.Workers;
using eKhaya.Services.Dtos;
using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.WorkerService
{
    public class WorkerAppService : IApplicationService , IWorkerAppService
    {
        private readonly IRepository<Worker, Guid> _workerRepository;
        private readonly IObjectMapper _objectMapper;

        public WorkerAppService(IRepository<Worker, Guid> workerRepository, IObjectMapper objectMapper)
        {
            _workerRepository = workerRepository;
            _objectMapper = objectMapper;
        }

        public async Task<WorkerDto> CreateWorkerAsync(CreateWorkerDto input)
        {
            var worker = _objectMapper.Map<Worker>(input);
            worker = await _workerRepository.InsertAsync(worker);
            return _objectMapper.Map<WorkerDto>(worker);
        }

        public async Task DeleteWorkerAsync(Guid id)
        {
            await _workerRepository.DeleteAsync(id);
        }

        public async Task<List<WorkerDto>> GetAllWorkersAsync()
        {
            var workers = await _workerRepository.GetAllListAsync();
            return _objectMapper.Map<List<WorkerDto>>(workers);
        }


        public async Task<WorkerDto> GetWorkerAsync(Guid id)
        {
           var worker = await _workerRepository.GetAsync(id);
            return _objectMapper.Map<WorkerDto>(worker);
        }

        public async Task<WorkerDto> UpdateWorkerAsync(WorkerDto input)
        {
            var worker = await _workerRepository.GetAsync(input.Id);
            worker = _objectMapper.Map(input, worker);
            worker = await _workerRepository.UpdateAsync(worker);
            return _objectMapper.Map<WorkerDto>(worker);
        }
    }
}
