using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Localization;
using AutoMapper.Internal.Mappers;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ResidentService
{
    public class ResidentAppService : ApplicationService , IResidentAppService
    {
        private readonly IRepository<Resident, Guid> _residentrepository;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Applicant, Guid> _applicantRepository; 

        public ResidentAppService(IRepository<Resident, Guid> residentrepository, UserManager userManager, RoleManager roleManager, IRepository<Applicant, Guid> applicantRepository)
        {
            _residentrepository = residentrepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _applicantRepository = applicantRepository;
        }

        public async Task<ResidentDto> CreateResidentAsync(CreateResidentDto input)
        {
            var user = ObjectMapper.Map<User>(input);
            user.UserName = input.EmailAddress;

            ObjectMapper.Map(input, user);
            if (!string.IsNullOrEmpty(user.NormalizedUserName) && !string.IsNullOrEmpty(user.NormalizedEmailAddress))
                user.SetNormalizedNames();
            user.TenantId = AbpSession.TenantId;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
            CheckErrors(await _userManager.CreateAsync(user, input.Password));
            input.RoleNames = new string[] { "Residents" };
            CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));

            await AssignRoleToUser(user, input.RoleNames);
            
            var resident = ObjectMapper.Map<Resident>(input);
            resident.User = user;
            await _residentrepository.InsertAsync(resident);
            CurrentUnitOfWork.SaveChanges();

            return ObjectMapper.Map<ResidentDto>(resident);


        }

        public async Task DeleteResidentAsync(Guid id)
        {
            await _residentrepository.DeleteAsync(id);
        }

        public async Task<ResidentDto> GetResidentAsync(Guid id)
        {
            var resident = await _residentrepository.GetAll().Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
            return ObjectMapper.Map<ResidentDto>(resident);
        }

        public async Task<List<ResidentDto>> GetAllResidentsAsync()
        {
            var resident = await _residentrepository.GetAllListAsync();
            return ObjectMapper.Map<List<ResidentDto>>(resident);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ResidentDto> UpdateResidentAsync(UpdateResidentDto input)
        {
            var resident = await _residentrepository.GetAsync(input.Id);
            var update = await _residentrepository.UpdateAsync(ObjectMapper.Map(input, resident));

            return ObjectMapper.Map<ResidentDto>(update);
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


        public async Task MakeaTenant(Guid userId)
        {
            // Retrieve the applicant by ID
            var applicant = await _applicantRepository
                .GetAllIncluding(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (applicant == null)
            {
                throw new Exception("Applicant not found");
            }

            // Retrieve the user associated with the applicant
            var user = await _userManager.FindByIdAsync(applicant.User.Id.ToString());
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Check if the user has the "Applicants" role
            var isInApplicantsRole = await _userManager.IsInRoleAsync(user, "Applicants");
            if (!isInApplicantsRole)
            {
                throw new Exception("User is not in the Applicants role");
            }

            // Remove the "Applicants" role and add the "Residents" role
            await _userManager.RemoveFromRoleAsync(user, "Applicants");
            await _userManager.AddToRoleAsync(user, "Residents");
        }


    }
}
