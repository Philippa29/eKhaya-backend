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

using Abp;
using eKhaya.Domain.Applications;
using eKhaya.Domain.Documents;
using Microsoft.AspNetCore.Identity;
using eKhaya.Domain.AgentsProperty;
using eKhaya.Domain.ENums;
using System.IO;


namespace eKhaya.Services.ApplicationsService
{

    public class ApplicationsAppService : ApplicationService , IApplicationAppService 

    {
        const string BASE_FILE_PATH = "App_Data/Documents";
        private readonly IRepository<Domain.Applications.Application, Guid> _applicationRepository;
        //private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Applicant, Guid> _applicantRepository;
        private readonly ISessionAppService _session;
        private readonly IRepository<Property, Guid> _propertyRepository;
        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly IRepository<AgentProperty, Guid> _propertyagentRepository;
       
        public ApplicationsAppService(IRepository<AgentProperty, Guid> propertyagentRepository, IRepository<Document, Guid> documentRepository , IRepository<Domain.Applications.Application, Guid> applicationRepository, IRepository<Applicant, Guid> applicantRepository, ISessionAppService session, IRepository<Property, Guid> propertyRepository)
        {
            _applicationRepository = applicationRepository;
            _propertyRepository = propertyRepository;
            _applicantRepository = applicantRepository;
            _session = session;
            _documentRepository = documentRepository;
            _propertyagentRepository = propertyagentRepository;
            
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
                ApplicationType = input.ApplicationType, 
                Property = property,
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

        public async Task<List<GetApplicationsDto>> GetAllApplicationsAsync()
        {
            var loginUser = await _session.GetCurrentLoginInformations();

            if (loginUser.User == null)
            {
                throw new Exception("User not found");
            }

            // Retrieve the logged-in user's username
            var username = loginUser.User.UserName;

            // Retrieve the properties for the logged-in user who is an agent
            var agentProperties = await _propertyagentRepository.GetAllIncluding(x => x.Agent.User, x => x.Property)
                .Where(x => x.Agent.User.UserName == username)
                .ToListAsync();

            if (agentProperties == null || !agentProperties.Any())
            {
                throw new Exception("Properties for agent not found");
            }

            var propertyIds = agentProperties.Select(ap => ap.Property).ToList();

            var applications = await _applicationRepository.GetAllIncluding(u => u.Applicant, u => u.Property)
                .Where(x => propertyIds.Contains(x.Property))
                .ToListAsync();

            var applicationDtos = applications.Select(application => new GetApplicationsDto
            {
                Id = application.Id,
                // Populate application properties
                Name = application.Applicant.Name,
                Surname = application.Applicant.Surname,
                Applicant = application.Applicant.Id,
                Property = application.Property.Id,
                ApplicationStatus = application.ApplicationStatus,
                MaritalStatus = application.MaritalStatus,
                CommunityType = application.ComunityType,
                CompanyName = application.CompanyName,
                CompanyAddress = application.CompanyAddress,
                CompanyContactNumber = application.CompanyContactNumber,
                Occupation = application.Occupation,
                Salary = application.Salary,
                MonthsWorked = application.MonthsWorked,
                CreatedDate = application.CreatedDate,
                Insolvent = application.Insolvent,
                Evicted = application.Evicited,
                ApplicationType = application.ApplicationType,

                // Populate property properties
                PropertyName = agentProperties.FirstOrDefault(ap => ap.Property.Id == application.Property.Id)?.Property?.PropertyName,
                UnitType = application.UnitType,

                // Populate applicant properties
                ApplicantEmail = application.Applicant.EmailAddress,
            }).ToList();

            return applicationDtos;
        }




        public async Task<ApplicationsDto> GetApplicationAsync(Guid id)
        {
            var application = await _applicationRepository.GetAllIncluding(x => x.Applicant, y => y.Property)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (application == null)
            {
                throw new Exception("Application not found");
            }

            var applicant = application.Applicant;
            var property = application.Property;

            var applicationDto = new ApplicationsDto
            {
                Id = application.Id,
                Name = applicant.Name,
                Surname = applicant.Surname,
                Applicant = applicant.Id,
                Property = property?.Id ?? Guid.Empty, 
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
                ApplicationType = application.ApplicationType,
            };

            return applicationDto;
        }

        public async Task<List<GetApplicationsDto>> GetApplicationsForLoggedInPersonAsync()
        {

            var loginUser = await _session.GetCurrentLoginInformations();

            if (loginUser.User == null)
            {
                throw new Exception("User not found");
            }

            // Retrieve the logged-in user's username
            var username = loginUser.User.UserName;
            // Retrieve the applications for the logged-in person
            var applications = await _applicationRepository
                .GetAllIncluding(x => x.Applicant, y => y.Property)
                .Where(a => a.Applicant.User.UserName == loginUser.User.UserName) // Assuming ApplicantId is the property that stores the applicant's user id
                .ToListAsync();

            if (applications == null || !applications.Any())
            {
                // Return an empty list if no applications are found for the logged-in person
                return new List<GetApplicationsDto>();
            }

            // Map the application entities to DTOs
            var applicationDtos = applications.Select(application => new GetApplicationsDto
            {
                Id = application.Id,
                Name = application.Applicant.Name,
                Surname = application.Applicant.Surname,
                Applicant = application.Applicant.Id,
                Property = application.Property?.Id ?? Guid.Empty,
                PropertyName = application.Property.PropertyName,
                ApplicationStatus = application.ApplicationStatus,
                MaritalStatus = application.MaritalStatus,
                CommunityType = application.ComunityType,
                CompanyName = application.CompanyName,
                CompanyAddress = application.CompanyAddress,
                CompanyContactNumber = application.CompanyContactNumber,
                Occupation = application.Occupation,
                Salary = application.Salary,
                MonthsWorked = application.MonthsWorked,
                CreatedDate = application.CreatedDate,
                Insolvent = application.Insolvent,
                Evicted = application.Evicited,
                ApplicationType = application.ApplicationType,
            }).ToList();

            return applicationDtos;
        }



        public async Task<GetApplicationsDto> UpdateApplicationAsync(ApplicationsDto input)
        {
            var application = await _applicationRepository.GetAllIncluding(x => x.Applicant).FirstOrDefaultAsync();
            var property = await _propertyRepository.FirstOrDefaultAsync(p => p.Id == input.Property);
            if (property == null)
            {
                throw new Exception("Property not found");
            }

            application.ApplicationStatus = input.ApplicationStatus;
            application.Property = property;
            application.MaritalStatus = input.MaritalStatus;
            application.ComunityType = input.ComunityType; // Corrected typo
            application.CompanyName = input.CompanyName;
            application.CompanyAddress = input.CompanyAddress;
            application.CompanyContactNumber = input.CompanyContactNumber;
            application.Occupation = input.Occupation;
            application.Salary = input.Salary;
            application.MonthsWorked = input.MonthsWorked;
            // application.CreatedDate = input.CreatedDate; // Removed if not intended to update
            application.Insolvent = input.Insolvent;
            application.Evicited = input.Evicted; // Corrected typo
                                                 // application.ApplicationType = Domain.ENums.ApplicationType.Submitted; // You might want to retrieve this from database or pass as parameter

            // Update the application entity in the repository
            var updatedApplication = await _applicationRepository.UpdateAsync(application);

            // Fetch applicant details from ApplicantRepository
            var applicantDetails = await _applicantRepository.GetAsync(application.Applicant.Id);

            // Create a new GetApplicationsDto object with updated information
            var updatedApplicationDto = new GetApplicationsDto
            {
                Id = updatedApplication.Id,
                Name = applicantDetails.Name,
                Surname = applicantDetails.Surname,
                Applicant = updatedApplication.Applicant.Id,
                Property = updatedApplication.Property.Id,
                ApplicationStatus = updatedApplication.ApplicationStatus,
                MaritalStatus = updatedApplication.MaritalStatus,
                CommunityType = updatedApplication.ComunityType,
                CompanyName = updatedApplication.CompanyName,
                CompanyAddress = updatedApplication.CompanyAddress,
                CompanyContactNumber = updatedApplication.CompanyContactNumber,
                Occupation = updatedApplication.Occupation,
                Salary = updatedApplication.Salary,
                MonthsWorked = updatedApplication.MonthsWorked,
                CreatedDate = updatedApplication.CreatedDate,
                Insolvent = updatedApplication.Insolvent,
                Evicted = updatedApplication.Evicited,
                ApplicationType = updatedApplication.ApplicationType,
                PropertyName = updatedApplication.Property.PropertyName,
                UnitType = updatedApplication.UnitType,
                ApplicantEmail = applicantDetails.EmailAddress // Assuming the email is available in the applicant details
            };

            // Return the updated application DTO
            return updatedApplicationDto;
        }










    }
}
