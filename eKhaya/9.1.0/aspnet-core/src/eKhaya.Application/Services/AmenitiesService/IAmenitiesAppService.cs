using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.AmenitiesService
{
    public interface IAmenitiesAppService: IApplicationService
    {
        Task<AmenitiesDto> CreateAmenitiesAsync(AmenitiesDto input);

        Task<AmenitiesDto> UpdateAmenitiesAsync(AmenitiesDto input);

        Task<AmenitiesDto> GetAmenitiesAsync(Guid id);

        Task<List<AmenitiesDto>> GetAllAmenitiesAsync();

        Task DeleteAmenitiesAsync(Guid id);
    }
}
