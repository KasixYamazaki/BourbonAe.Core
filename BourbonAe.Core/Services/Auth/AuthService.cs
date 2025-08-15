using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BourbonAe.Core.Services.Auth
{
    // Minimal User entity (map to your actual schema in ApplicationDbContext)
    public class User
    {
        public string UserId { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!; // format: {iterations}.{saltBase64}.{hashBase64}
        public bool IsActive { get; set; } = true;
        public string Role { get; set; } = "User";
        public DateTime? LastLoginAt { get; set; }
    }

    public interface IAppDb
    {
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }

    public sealed class AuthService : IAuthService
    {
        private readonly IAppDb _db;
        public AuthService(IAppDb db) => _db = db;

        public async Task<(bool ok, ClaimsPrincipal? principal, string? error)> SignInAsync(string userId, string password, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrEmpty(password))
                return (false, null, "ユーザーIDとパスワードを入力してください。");

            var user = await _db.Users.AsQueryable().FirstOrDefaultAsync(u => u.UserId == userId, ct);
            if (user is null) return (false, null, "ユーザーが見つかりません。");
            if (!user.IsActive) return (false, null, "アカウントが無効です。管理者に連絡してください。");

            if (!VerifyPassword(password, user.PasswordHash))
                return (false, null, "ユーザーIDまたはパスワードが正しくありません。");

            user.LastLoginAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(ct);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);
            return (true, principal, null);
        }

        public async Task<DateTime?> GetLastLoginAsync(string userId, CancellationToken ct = default)
        {
            var user = await _db.Users.AsQueryable().Where(u => u.UserId == userId)
                .Select(u => new { u.LastLoginAt }).FirstOrDefaultAsync(ct);
            return user?.LastLoginAt;
        }

        // ========== PBKDF2 helpers ==========
        public static string HashPassword(string password, int iterations = 100_000, int saltBytes = 16, int keyBytes = 32)
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[saltBytes];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(keyBytes);

            return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        public static bool VerifyPassword(string password, string stored)
        {
            // format: iterations.saltBase64.hashBase64
            var parts = stored.Split('.', 3);
            if (parts.Length != 3) return false;
            if (!int.TryParse(parts[0], out var iterations)) return false;
            var salt = Convert.FromBase64String(parts[1]);
            var expected = Convert.FromBase64String(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var actual = pbkdf2.GetBytes(expected.Length);
            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }
    }
}
