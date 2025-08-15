using BourbonAe.Core.Data;
using BourbonAe.Core.Models.Entities;
using BourbonAe.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Xunit;

namespace BourbonAe.Core.Tests
{
    public class DataAccessServiceTests
    {
        [Fact]
        public async Task CreateWhere_And_AddWhere_Works()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;
            var context = new ApplicationDbContext(options);
            var config = new ConfigurationBuilder().AddInMemoryCollection(
                new Dictionary<string, string?> { { "ConnectionStrings:DefaultConnection", "Server=(local);Database=Test;Trusted_Connection=True;" } }
            ).Build();

            var service = new DataAccessService(context, config);
            var where = service.CreateWhere(new[] { "COL1 = 'A'", "COL2 = 'B'" });
            Assert.Equal(" WHERE COL1 = 'A' AND COL2 = 'B'", where);

            var where2 = service.AddWhere(where, "COL3 = 'C'");
            Assert.Equal(" WHERE COL1 = 'A' AND COL2 = 'B' AND COL3 = 'C'", where2);
        }
    }
}
