using Abp.Application.Services;
using Abp.Domain.Repositories;

using eKhaya.Domain.Applications;
using eKhaya.Domain.ENums;
using eKhaya.Domain.Units;
using eKhaya.Domain.Users;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ApplicationsService
{
    public class ApplicationsAppService : ApplicationService , IApplicationAppService

    {
        private readonly IRepository<Application, Guid> _applicationRepository; 
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IRepository<Applicant, Guid> _applicantRepository;

        public ApplicationsAppService(IRepository<Application, Guid> applicationRepository, IRepository<Unit, Guid> unitRepository, IRepository<Applicant, Guid> applicantRepository)
        {
            _applicationRepository = applicationRepository;
            _unitRepository = unitRepository;
            _applicantRepository = applicantRepository;
        }

        public async Task<ApplicationsDto> CreateApplicationAsync(CreateApplicationDto input)
        {

            var unit = await _unitRepository.GetAsync(input.Unit);
            var applicant = await _applicantRepository.GetAsync(input.Applicant);

            if(unit == null)
            {
                throw new Exception("Unit not found");
            }

            if(applicant == null)
            {
                throw new Exception("Applicant not found");
            }

            var application = new Application
            {
                  Unit  = unit,
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
                  CreatedDate = input.CreatedDate,
                  Insolvent = input.Insolvent,
                  Evicited = input.Evicited,
                  Declaration = input.Declaration,
                  TermsandConditions = input.TermsandConditions,
                  ApplicationType = input.ApplicationType
              };

            await _applicationRepository.InsertAsync(application);

            return ObjectMapper.Map<ApplicationsDto>(application);
           
        }
          

        public async Task DeleteApplicationAsync(Guid id)
        {
            await _applicationRepository.DeleteAsync(id);
        }

        public async Task<List<ApplicationsDto>> GetAllApplicationsAsync()
        {
            var applications = await _applicationRepository.GetAllIncluding( x => x.Unit, x => x.Applicant).ToListAsync();
            return ObjectMapper.Map<List<ApplicationsDto>>(applications);
            
        }

        public async Task<ApplicationsDto> GetApplicationAsync(Guid id)
        {
            var application = await _applicationRepository.GetAllIncluding(x => x.Unit , x => x.Applicant).FirstOrDefaultAsync();

            return ObjectMapper.Map<ApplicationsDto>(application);
           
                
        }

        public async Task<ApplicationsDto> UpdateApplicationAsync(ApplicationsDto input)
        {
            var application = await _applicationRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, application);
            await _applicationRepository.UpdateAsync(application);

            return ObjectMapper.Map<ApplicationsDto>(application);
        }


    }
}
