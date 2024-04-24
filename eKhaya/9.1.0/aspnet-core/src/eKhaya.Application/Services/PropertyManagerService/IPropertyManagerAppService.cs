using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.ProjectManagerService
{
    public interface IPropertyManagerAppService : IApplicationService
    {
        Task<PropertyManagerDto> CreatePropertyManagerAsync(CreatePropertyManagerDto input);

        Task<PropertyManagerDto> UpdatePropertyManagerAsync(PropertyManagerDto input);

        Task<PropertyManagerDto> GetPropertyManagerAsync(Guid id);

        Task<List<PropertyManagerDto>> GetAllPropertyManagersAsync();

        Task DeletePropertyManagerAsync(Guid id);

    }
}
