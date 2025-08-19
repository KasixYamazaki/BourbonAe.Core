using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using BourbonAe.Core.Data;
using BourbonAe.Core.Data.Entities;
using BourbonAe.Core.Models.AEMM0010;
using BourbonAe.Core.Services.Features.AEMM0010;
using BourbonAe.Core.Tests.Unit.Helpers;

namespace BourbonAe.Core.Tests.Unit.Services
{
    public class Aemm0010ServiceTests
    {
        [Fact]
        public async Task Upsert_Find_Delete_Lifecycle()
        {
            var (opts, conn) = SqliteInMemory.CreateOptions<ApplicationDbContext>();
            await using var db = new ApplicationDbContext(opts);
            await db.Database.EnsureCreatedAsync();

            var svc = new Aemm0010Service(db);

            await svc.UpsertAsync(new Aemm0010EditRow { Code = "K01", Name = "区分1", IsActive = true });
            (await svc.FindAsync("K01"))!.Name.Should().Be("区分1");

            (await svc.DeleteAsync("K01")).Should().Be(1);
            (await svc.FindAsync("K01")).Should().BeNull();
        }
    }
}
