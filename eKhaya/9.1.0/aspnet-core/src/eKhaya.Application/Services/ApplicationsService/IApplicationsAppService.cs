using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ApplicationsService
{
     public interface IApplicationAppService : IApplicationService
    {
        public Task<ApplicationsDto> CreateApplicationAsync(ApplicationsDto input);

        public Task<ApplicationsDto> UpdateApplicationAsync(ApplicationsDto input);

        public Task<ApplicationsDto> GetApplicationAsync(Guid id);

        public Task<List<ApplicationsDto>> GetAllApplicationsAsync();

        public Task DeleteApplicationAsync(Guid id);

    }
}
