using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using eKhaya.Authorization.Roles;
using eKhaya.Authorization.Users;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ApplicantService
{
    public class ApplicantAppService : ApplicationService , IApplicantAppService
    {
        private readonly IRepository<Applicant, Guid> _applicantRepository; 
        private readonly UserManager _userManager ;
        private readonly RoleManager _roleManager; 

        public ApplicantAppService(IRepository<Applicant, Guid> applicantRepository, UserManager userManager, RoleManager roleManager)
        {
            _applicantRepository = applicantRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ApplicantDto> CreateApplicantAsync(CreateApplicantDto input)
        {
            var user = ObjectMapper.Map<User>(input);
            user.UserName = input.EmailAddress;

            ObjectMapper.Map(input, user);
            if (!string.IsNullOrEmpty(user.NormalizedUserName) && !string.IsNullOrEmpty(user.NormalizedEmailAddress))
                user.SetNormalizedNames();
            user.TenantId = AbpSession.TenantId;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
            CheckErrors(await _userManager.CreateAsync(user, input.Password));
            input.RoleNames = new string[] { "Applicants" };
            CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));

            await AssignRoleToUser(user, input.RoleNames);

            var applicant = ObjectMapper.Map<Applicant>(input);
            applicant.ApplicantID = input.ApplicantId; 
            applicant.User = user;
            await _applicantRepository.InsertAsync(applicant);
            CurrentUnitOfWork.SaveChanges();

            return ObjectMapper.Map<ApplicantDto>(applicant);



        }

        public async Task DeleteApplicantAsync(Guid id)
        {
            await _applicantRepository.DeleteAsync(id);
        }

        public async Task<List<ApplicantDto>> GetAllApplicantsAsync()
        {
            var applicants = await _applicantRepository.GetAllListAsync();
            return ObjectMapper.Map<List<ApplicantDto>>(applicants);
        }

        public async Task<ApplicantDto> GetApplicantAsync(Guid id)
        {
            var applicant = await _applicantRepository.GetAsync(id);
            return ObjectMapper.Map<ApplicantDto>(applicant);
        }

        public async Task<ApplicantDto> UpdateApplicantAsync(UpdateApplicantDto input)
        {
            var applicant = await _applicantRepository.GetAsync(input.Id);
            var update = await _applicantRepository.UpdateAsync(ObjectMapper.Map(input, applicant));
            return ObjectMapper.Map<ApplicantDto>(update);
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
