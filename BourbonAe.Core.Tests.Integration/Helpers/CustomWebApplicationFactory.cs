using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using BourbonAe.Core.Data;

namespace BourbonAe.Core.Tests.Integration.Helpers
{
    /// <summary>
    /// Boots the real Program/DI but swaps ApplicationDbContext to in-memory Sqlite and injects a fake auth scheme.
    /// </summary>
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace ApplicationDbContext registration with in-memory Sqlite
                var dbDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (dbDescriptor != null) services.Remove(dbDescriptor);

                var conn = new SqliteConnection("Filename=:memory:");
                conn.Open();

                services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(conn));
                services.AddScoped<DbContext>(_ => _.GetRequiredService<ApplicationDbContext>());
                services.AddScoped<IAppDb>(_ => _.GetRequiredService<ApplicationDbContext>());

                // Fake authentication
                services.AddAuthentication("TestScheme")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", _ => { });

                services.PostConfigure<Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions>(
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                    opt =>
                    {
                        // For tests, don't redirect on 401; surface status codes
                        opt.Events.OnRedirectToLogin = ctx => { ctx.Response.StatusCode = 401; return System.Threading.Tasks.Task.CompletedTask; };
                    });

                // Build to create the database schema
                using var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}
