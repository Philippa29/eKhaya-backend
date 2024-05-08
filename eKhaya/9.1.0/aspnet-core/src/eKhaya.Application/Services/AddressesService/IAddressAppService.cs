using Abp.Application.Services;
using eKhaya.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.AddressesService
{
    public interface IAddressAppService : IApplicationService
    {
        Task<AddressesDto> CreateAddressAsync(AddressesDto input);

        Task<AddressesDto> UpdateAddressAsync(AddressesDto input);

        Task<AddressesDto> GetAddressAsync(Guid id);

        Task<List<GetAllAddressesDto>> GetAllAddressesAsync();

        Task DeleteAddressAsync(Guid id);
    }
}
