using System.Threading.Tasks;
using eKhaya.Configuration.Dto;

namespace eKhaya.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
