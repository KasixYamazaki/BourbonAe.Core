using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using BourbonAe.Core.Data;
using BourbonAe.Core.Data.Entities;
using BourbonAe.Core.Models.AESJ1110;
using BourbonAe.Core.Services.Features.AESJ1110;
using BourbonAe.Core.Tests.Unit.Helpers;
using Xunit;

namespace BourbonAe.Core.Tests.Unit.Services
{
    public class Aesj1110ServiceTests
    {
        [Fact]
        public async Task Search_FiltersByCustomerCode_And_UpdatesStatus()
        {
            var (opts, conn) = SqliteInMemory.CreateOptions<ApplicationDbContext>();
            await using var db = new ApplicationDbContext(opts);
            await db.Database.EnsureCreatedAsync();

            db.AddRange(
                new Aesj1110Entity { SlipNo = "1001", CustomerCode = "C01", CustomerName = "AAA", Date = new DateTime(2024,1,2), Status = "未確定", Amount = 1000 },
                new Aesj1110Entity { SlipNo = "1002", CustomerCode = "C02", CustomerName = "BBB", Date = new DateTime(2024,1,3), Status = "未確定", Amount = 2000 }
            );
            await db.SaveChangesAsync();

            var svc = new Aesj1110Service(db);

            var found = await svc.SearchAsync(new Aesj1110Filter { CustomerCode = "C02" });
            found.Should().ContainSingle(x => x.SlipNo == "1002");

            var updated = await svc.SaveAsync(new[] { "1002" }); // e.g., confirm slips
            updated.Should().Be(1);

            var after = await db.Aesj1110Entities.AsNoTracking().SingleAsync(x => x.SlipNo == "1002");
            after.Status.Should().Be("確定");
        }
    }
}
