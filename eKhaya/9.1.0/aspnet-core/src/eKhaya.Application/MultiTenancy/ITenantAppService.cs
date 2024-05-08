using Abp.Application.Services;
using eKhaya.MultiTenancy.Dto;

namespace eKhaya.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

