using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using eKhaya.Authorization.Roles;
using eKhaya.Authorization.Users;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ProjectManagerService
{
    public class PropertyManagerAppServicem : ApplicationService, IPropertyManagerAppService
    {
        private readonly IRepository<PropertyManager, Guid> _propertyManagerRepository; 
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager; 

        public PropertyManagerAppServicem(IRepository<PropertyManager, Guid> propertyManagerRepository, UserManager userManager, RoleManager roleManager)
        {
            _propertyManagerRepository = propertyManagerRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<PropertyManagerDto> CreatePropertyManagerAsync(CreatePropertyManagerDto input)
        {
            var user = ObjectMapper.Map<User>(input);
            user.UserName = input.EmailAddress;

            ObjectMapper.Map(input, user);
            if (!string.IsNullOrEmpty(user.NormalizedUserName) && !string.IsNullOrEmpty(user.NormalizedEmailAddress))
                user.SetNormalizedNames();
            user.TenantId = AbpSession.TenantId;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
            CheckErrors(await _userManager.CreateAsync(user, input.Password));
            input.RoleNames = new string[] { "PropertyManager" };
            CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));


            await AssignRoleToUser(user, input.RoleNames);

            var manager = ObjectMapper.Map<PropertyManager>(input);
            manager.User = user;
            await _propertyManagerRepository.InsertAsync(manager);
            CurrentUnitOfWork.SaveChanges();

            return ObjectMapper.Map<PropertyManagerDto>(manager);
        }

        public async Task  DeletePropertyManagerAsync(Guid id)
        {
           await _propertyManagerRepository.DeleteAsync(id);
        }

        public async Task<List<PropertyManagerDto>> GetAllPropertyManagersAsync()
        {
              var managers = _propertyManagerRepository.GetAllListAsync();
            return ObjectMapper.Map<List<PropertyManagerDto>>(managers);
        }

        public async Task<PropertyManagerDto> GetPropertyManagerAsync(Guid id)
        {
           var manager = await _propertyManagerRepository.GetAsync(id);
            return ObjectMapper.Map<PropertyManagerDto>(manager);
        }

        public async Task<PropertyManagerDto> UpdatePropertyManagerAsync(PropertyManagerDto input)
        {
           var manager = ObjectMapper.Map<PropertyManager>(input);
            await _propertyManagerRepository.UpdateAsync(manager);
            return ObjectMapper.Map<PropertyManagerDto>(manager);
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
