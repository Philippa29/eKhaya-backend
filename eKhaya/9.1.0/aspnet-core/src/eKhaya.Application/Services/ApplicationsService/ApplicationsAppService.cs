using Abp.Application.Services;
using Abp.Domain.Repositories;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using System.Text;
using System.Threading.Tasks;
using eKhaya.Sessions;
using Abp.Authorization;
using eKhaya.Domain.Properties;
using static System.Net.Mime.MediaTypeNames;
using Abp;
using eKhaya.Domain.Applications;

namespace eKhaya.Services.ApplicationsService
{

    public class ApplicationsAppService : ApplicationService , IApplicationAppService

    {
        private readonly IRepository<Domain.Applications.Application, Guid> _applicationRepository; 
        
        private readonly IRepository<Applicant, Guid> _applicantRepository;
        private readonly ISessionAppService _session;
        private readonly IRepository<Property, Guid> _propertyRepository;

        public ApplicationsAppService(IRepository<Domain.Applications.Application, Guid> applicationRepository, IRepository<Applicant, Guid> applicantRepository, ISessionAppService session, IRepository<Property, Guid> propertyRepository)
        {
            _applicationRepository = applicationRepository;
            _propertyRepository = propertyRepository;
            _applicantRepository = applicantRepository;
            _session = session;
        }

        [AbpAuthorize("Pages.Applicants")]
        public async Task<ApplicationsDto> CreateApplicationAsync(CreateApplicationDto input)
        {

            var loginUser = await _session.GetCurrentLoginInformations();

            if (loginUser.User == null)
            {
                throw new Exception("User not found");
            }

            var applicant = await _applicantRepository.FirstOrDefaultAsync(a => a.User.UserName == loginUser.User.UserName);

            if (applicant == null)
            {
                throw new Exception("Applicant not found");
            }

            var property = await _propertyRepository.FirstOrDefaultAsync(p => p.Id == input.Property);
            if (property == null)
            {
                throw new Exception("Property not found");
            }

            // Check if the applicant already has an existing application
            var existingApplication = await _applicationRepository.FirstOrDefaultAsync(app => app.Applicant.Id == applicant.Id);
            if (existingApplication != null)
            {
                // Return DTO filled with existing application data

                var exisitingapplicationDto = new ApplicationsDto
                {
                    Id = existingApplication.Id,
                    Name = applicant.Name,
                    Surname = applicant.Surname,
                    Applicant = applicant.Id,
                    Property = property.Id,
                    ApplicationStatus = existingApplication.ApplicationStatus,
                    MaritalStatus = existingApplication.MaritalStatus,
                    ComunityType = existingApplication.ComunityType,
                    CompanyName = existingApplication.CompanyName,
                    CompanyAddress = existingApplication.CompanyAddress,
                    CompanyContactNumber = existingApplication.CompanyContactNumber,
                    Occupation = existingApplication.Occupation,
                    Salary = existingApplication.Salary,
                    MonthsWorked = existingApplication.MonthsWorked,
                    CreatedDate = existingApplication.CreatedDate,
                    Insolvent = existingApplication.Insolvent,
                    Evicted = existingApplication.Evicited,
                    ApplicationType = existingApplication.ApplicationType
                };
                return exisitingapplicationDto; 
            }

            // Proceed with creating a new application
            

            var application = new Domain.Applications.Application
            {
                Applicant = applicant,
                ApplicationStatus = input.ApplicationStatus,
                MaritalStatus = input.MaritalStatus,
                ComunityType = input.ComunityType,
                CompanyName = input.CompanyName,
                CompanyAddress = input.CompanyAddress,
                CompanyContactNumber = input.CompanyContactNumber,
                Occupation = input.Occupation,
                Salary = input.Salary,
                MonthsWorked = input.MonthsWorked,
                CreatedDate = DateTime.Now,
                Insolvent = input.Insolvent,
                Evicited = input.Evicited,
                ApplicationType = input.ApplicationType
            };

            await _applicationRepository.InsertAsync(application);

            var applicationDto = new ApplicationsDto
            {
                Id = application.Id, 
                Name = applicant.Name,
                Surname = applicant.Surname,
                Applicant = applicant.Id,
                Property = property.Id, 
                ApplicationStatus = application.ApplicationStatus,
                MaritalStatus = application.MaritalStatus,
                ComunityType = application.ComunityType,
                CompanyName = application.CompanyName,
                CompanyAddress = application.CompanyAddress,
                CompanyContactNumber = application.CompanyContactNumber,
                Occupation = application.Occupation,
                Salary = application.Salary,
                MonthsWorked = application.MonthsWorked,
                CreatedDate = application.CreatedDate,
                Insolvent = application.Insolvent,
                Evicted = application.Evicited,
                ApplicationType = application.ApplicationType
            };

            return applicationDto;
           
        }
          

        public async Task DeleteApplicationAsync(Guid id)
        {
            await _applicationRepository.DeleteAsync(id);
        }

        public async Task<List<ApplicationsDto>> GetAllApplicationsAsync()
        {
            var applications = await _applicationRepository.GetAllIncluding( x => x.Applicant).ToListAsync();
            return ObjectMapper.Map<List<ApplicationsDto>>(applications);
            
        }

        public async Task<ApplicationsDto> GetApplicationAsync(Guid id)
        {
            var application = await _applicationRepository.GetAllIncluding( x => x.Applicant).FirstOrDefaultAsync();

            return ObjectMapper.Map<ApplicationsDto>(application);
           
                
        }

        public async Task<ApplicationsDto> UpdateApplicationAsync(ApplicationsDto input)
        {
            var application = await _applicationRepository.GetAsync(input.Id);

            application.ApplicationStatus = input.ApplicationStatus;
            application.MaritalStatus = input.MaritalStatus;
            application.ComunityType = input.ComunityType;
            application.CompanyName = input.CompanyName;
            application.CompanyAddress = input.CompanyAddress;
            application.CompanyContactNumber = input.CompanyContactNumber;
            application.Occupation = input.Occupation;
            application.Salary = input.Salary;
            application.MonthsWorked = input.MonthsWorked;
            application.CreatedDate = input.CreatedDate;
            application.Insolvent = input.Insolvent;
            application.Evicited = input.Evicted;
            application.ApplicationType = Domain.ENums.ApplicationType.Submitted;

            // Update the application entity in the repository
            var updatedApplication = await _applicationRepository.UpdateAsync(application);

            // Map the updated application entity back to DTO and return
            return ObjectMapper.Map<ApplicationsDto>(updatedApplication);
        }




    }
}
