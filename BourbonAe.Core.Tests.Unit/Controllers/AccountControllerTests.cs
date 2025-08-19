using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;
using BourbonAe.Core.Services.Auth;
using BourbonAe.Core.Controllers;
using BourbonAe.Core.Models.Auth;
using Xunit;

namespace BourbonAe.Core.Tests.Unit.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task Login_Success_RedirectsToHome()
        {
            var mock = new Mock<IAuthService>();
            mock.Setup(x => x.ValidateAsync("kasix", "pass")).ReturnsAsync(true);
            var controller = new AccountController(mock.Object);
            var vm = new LoginViewModel { UserId = "kasix", Password = "pass" };

            var result = await controller.Login(vm);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Login_Failure_ReturnsViewWithError()
        {
            var mock = new Mock<IAuthService>();
            mock.Setup(x => x.ValidateAsync("kasix", "bad")).ReturnsAsync(false);
            var controller = new AccountController(mock.Object);
            var vm = new LoginViewModel { UserId = "kasix", Password = "bad" };

            var result = await controller.Login(vm) as ViewResult;
            result.Should().NotBeNull();
            controller.ModelState.IsValid.Should().BeFalse();
        }
    }
}
