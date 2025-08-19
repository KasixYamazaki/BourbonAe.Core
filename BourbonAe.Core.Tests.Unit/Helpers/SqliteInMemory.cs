using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BourbonAe.Core.Tests.Unit.Helpers
{
    public static class SqliteInMemory
    {
        public static (DbContextOptions<T> Options, SqliteConnection Conn) CreateOptions<T>() where T : DbContext
        {
            var conn = new SqliteConnection("Filename=:memory:");
            conn.Open();
            var builder = new DbContextOptionsBuilder<T>().UseSqlite(conn);
            return (builder.Options, conn);
        }
    }
}
