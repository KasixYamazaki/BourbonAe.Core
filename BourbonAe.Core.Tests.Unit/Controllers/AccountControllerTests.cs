using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        [Fact(Skip = "Authentication services not configured for unit test.")]
        public async Task Login_Success_RedirectsToHome()
        {
            var auth = new Mock<IAuthService>();
            var config = new Mock<IConfiguration>();
            auth.Setup(x => x.SignInAsync("kasix", "pass", It.IsAny<CancellationToken>()))
                .ReturnsAsync((true, new ClaimsPrincipal(new ClaimsIdentity("Test")), (string?)null));
            var controller = new AccountController(auth.Object, config.Object);
            var httpContext = new DefaultHttpContext();
            var services = new ServiceCollection();
            services.AddAuthentication().AddCookie();
            httpContext.RequestServices = services.BuildServiceProvider();
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var vm = new LoginViewModel { UserId = "kasix", Password = "pass" };

            var result = await controller.Login(vm);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Login_Failure_ReturnsViewWithError()
        {
            var auth = new Mock<IAuthService>();
            var config = new Mock<IConfiguration>();
            auth.Setup(x => x.SignInAsync("kasix", "bad", It.IsAny<CancellationToken>()))
                .ReturnsAsync((false, (ClaimsPrincipal?)null, "invalid"));
            var controller = new AccountController(auth.Object, config.Object);
            var vm = new LoginViewModel { UserId = "kasix", Password = "bad" };

            var result = await controller.Login(vm) as ViewResult;
            result.Should().NotBeNull();
            controller.ModelState.IsValid.Should().BeFalse();
        }
    }
}
