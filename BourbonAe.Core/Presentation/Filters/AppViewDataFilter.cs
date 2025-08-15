using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
namespace BourbonAe.Core.Presentation.Filters
{
    public sealed class AppViewDataFilter : IAsyncActionFilter
    {
        private readonly IConfiguration _config;
        public AppViewDataFilter(IConfiguration config) => _config = config;
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is Controller controller)
            {
                controller.ViewData["ApplicationTitle"] = _config["ApplicationTitle"] ?? "BourbonAe";
                controller.ViewData["TempFileDir"] = _config["TempFileDir"];
            }
            await next();
        }
    }
}
