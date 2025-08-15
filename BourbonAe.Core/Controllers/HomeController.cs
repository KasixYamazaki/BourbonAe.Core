using Microsoft.AspNetCore.Mvc;

namespace BourbonAe.Core.Controllers
{
    /// <summary>
    /// Simple home controller that serves the default landing page of the
    /// migrated application. As the migration progresses, additional
    /// controllers will be added to mirror the WebForms pages.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Displays the home page. This method currently renders a
        /// placeholder view. Replace or expand this view as you migrate
        /// additional features from the original application.
        /// </summary>
        /// <returns>An IActionResult rendering the Index view.</returns>
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }
    }
}