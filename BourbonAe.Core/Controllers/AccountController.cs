using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BourbonAe.Core.Models.Auth;
using BourbonAe.Core.Services.Auth;

namespace BourbonAe.Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IConfiguration _config;

        public AccountController(IAuthService auth, IConfiguration config)
        {
            _auth = auth;
            _config = config;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (ok, principal, error) = await _auth.SignInAsync(model.UserId, model.Password);
            if (!ok || principal is null)
            {
                ModelState.AddModelError(string.Empty, error ?? "サインインに失敗しました。");
                return View(model);
            }

            var props = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(7) : (DateTimeOffset?)null
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            // 既定の遷移先（Home/Index）
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Web.config の Logout 設定相当を appsettings.json で持つ場合
            var redirect = _config["LogoutRedirectPath"];
            if (!string.IsNullOrWhiteSpace(redirect) && Url.IsLocalUrl(redirect))
                return Redirect(redirect);

            return RedirectToAction("Login", "Account");
        }
    }
}
