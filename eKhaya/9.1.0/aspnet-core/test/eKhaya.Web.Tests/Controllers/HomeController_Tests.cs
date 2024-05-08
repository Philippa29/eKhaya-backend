using System.Threading.Tasks;
using eKhaya.Models.TokenAuth;
using eKhaya.Web.Controllers;
using Shouldly;
using Xunit;

namespace eKhaya.Web.Tests.Controllers
{
    public class HomeController_Tests: eKhayaWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}