using System.Threading.Tasks;
using Abp.Application.Services;
using eKhaya.Sessions.Dto;

namespace eKhaya.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
