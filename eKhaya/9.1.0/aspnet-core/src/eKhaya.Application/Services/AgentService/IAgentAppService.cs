using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.AgentService
{
    public interface IAgentAppService : IApplicationService
    {
        Task<AgentDto> CreateAgentAsync(CreateAgentDto input);

        Task<AgentDto> UpdateAgentAsync(UpdateAgentDto input);

        Task<AgentDto> GetAgentAsync(Guid id);

        Task<List<AgentDto>> GetAllAgentsAsync();

        Task DeleteAgentAsync(Guid id);


    }
}
