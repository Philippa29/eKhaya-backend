using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.PropertyAgentsService
{
    public interface IPropertyAgentAppService : IApplicationService
    {

        public Task<PropertyAgentsDto> CreatePropertyAgentAsync(PropertyAgentsDto input);

        public Task<PropertyAgentsDto> UpdatePropertyAgentAsync(PropertyAgentsDto input);

        public Task<PropertyAgentsDto> GetPropertyAgentAsync(Guid id);

        public Task<List<PropertyAgentsDto>> GetAllAgentsPerProperty(Guid id);

        public Task DeletePropertyAgentAsync(Guid id);
    }
}
