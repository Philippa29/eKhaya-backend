using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ApplicantService
{
    public interface IApplicantAppService : IApplicationService

    {
        Task<ApplicantDto> CreateApplicantAsync(CreateApplicantDto input);

        Task<ApplicantDto> UpdateApplicantAsync(ApplicantDto input);

        Task<ApplicantDto> GetApplicantAsync(Guid id);

        Task<List<ApplicantDto>> GetAllApplicantsAsync();

        Task DeleteApplicantAsync(Guid id);
    }
}
