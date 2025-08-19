using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using BourbonAe.Core.Tests.Integration.Helpers;

namespace BourbonAe.Core.Tests.Integration.Endpoints
{
    public class BasicFlowTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        public BasicFlowTests(CustomWebApplicationFactory factory) => _factory = factory;

        [Fact]
        public async Task Get_Home_Index_ReturnsOk()
        {
            var client = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions { AllowAutoRedirect = true });
            var res = await client.GetAsync("/");
            res.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Post_Reports_ExportExcel_ReturnsFile()
        {
            var client = _factory.CreateClient();
            // NOTE: if AntiForgery is enabled, we can disable in test or supply a token. Here we expect the action to not require it in test env.
            var res = await client.PostAsync("/Reports/ExportExcel", new FormUrlEncodedContent(new []{ new KeyValuePair<string,string>("dummy","1") }));
            res.IsSuccessStatusCode.Should().BeTrue();
            res.Content.Headers.ContentType!.ToString().Should().Contain("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
