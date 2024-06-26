﻿using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ApplicationsService
{
     public interface IApplicationAppService : IApplicationService
    {

        public Task<ApplicationsDto> CreateApplicationAsync(CreateApplicationDto input);


        public Task<GetApplicationsDto> UpdateApplicationAsync(ApplicationsDto input); 

        public Task<ApplicationsDto> GetApplicationAsync(Guid id);

        public Task<List<GetApplicationsDto>> GetAllApplicationsAsync();

        public Task DeleteApplicationAsync(Guid id);

        Task<List<GetApplicationsDto>> GetApplicationsForLoggedInPersonAsync(); 




    }
}
