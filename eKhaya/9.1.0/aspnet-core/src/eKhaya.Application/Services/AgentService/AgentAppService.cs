using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using eKhaya.Authorization.Roles;
using eKhaya.Authorization.Users;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.AgentService
{
    public class AgentAppService : ApplicationService, IAgentAppService

    { 
        private readonly IRepository<Agent, Guid> _agentRepository;
        private readonly UserManager _userManager; 
        private readonly RoleManager _roleManager;

        public AgentAppService(IRepository<Agent, Guid> agentRepository, UserManager userManager, RoleManager roleManager)
        {
            _agentRepository = agentRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<AgentDto> CreateAgentAsync(CreateAgentDto input)
        {
            var user = ObjectMapper.Map<User>(input);
            user.UserName = input.EmailAddress;

            ObjectMapper.Map(input, user);
            if (!string.IsNullOrEmpty(user.NormalizedUserName) && !string.IsNullOrEmpty(user.NormalizedEmailAddress))
                user.SetNormalizedNames();
            user.TenantId = AbpSession.TenantId;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
            CheckErrors(await _userManager.CreateAsync(user, input.Password));
            input.RoleNames = new string[] { "Agents" };
            CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));

            await AssignRoleToUser(user, input.RoleNames);

            var agent = ObjectMapper.Map<Agent>(input);
            agent.User = user;
            await _agentRepository.InsertAsync(agent);
            CurrentUnitOfWork.SaveChanges();

            return ObjectMapper.Map<AgentDto>(agent);   
            
        }

        public async Task DeleteAgentAsync(Guid id)
        {
            await _agentRepository.DeleteAsync(id);
        }

        public async Task<List<AgentDto>> GetAllAgentsAsync()
        {
           var agents = await _agentRepository.GetAllListAsync();
            return ObjectMapper.Map<List<AgentDto>>(agents);
        }

        public Task<AgentDto> GetAgentAsync(Guid id)
        {
            var agent = _agentRepository.GetAsync(id);
            return Task.FromResult(ObjectMapper.Map<AgentDto>(agent));

        }

        
        public async Task<AgentDto> UpdateAgentAsync(UpdateAgentDto input)
        {
          var agent = await _agentRepository.GetAsync(input.Id);
            var update = await _agentRepository.UpdateAsync(ObjectMapper.Map(input,agent));
            return ObjectMapper.Map<AgentDto>(update);
        }


        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        private async Task AssignRoleToUser(User user, string[] roleNames)
        {
            var roles = await _roleManager.Roles.Where(r => roleNames.Contains(r.Name)).ToListAsync();
            var roleNamesToAdd = roles.Select(r => r.Name);
            await _userManager.AddToRolesAsync(user, roleNamesToAdd);
        }
    }
}
