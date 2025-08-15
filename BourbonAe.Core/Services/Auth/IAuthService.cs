using System.Security.Claims;

namespace BourbonAe.Core.Services.Auth
{
    public interface IAuthService
    {
        Task<(bool ok, ClaimsPrincipal? principal, string? error)> SignInAsync(string userId, string password, CancellationToken ct = default);
        Task<DateTime?> GetLastLoginAsync(string userId, CancellationToken ct = default);
    }
}
