using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using BourbonAe.Core.Data;

namespace BourbonAe.Core.Services
{
    public class DataAccessService : IDataAccessService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public DataAccessService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<List<T>> QueryAsync<T>(Func<IQueryable<T>, IQueryable<T>>? filter = null) where T : class
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (filter != null)
            {
                query = filter(query);
            }
            return await query.ToListAsync();
        }

        public async Task<DataTable> RawQueryAsync(string sql, object? parameters = null)
        {
            var table = new DataTable();
            var conStr = IDataAccessService.GetDbConStr(_config);

            using var connection = new SqlConnection(conStr);
            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);
            if (parameters != null)
            {
                foreach (var prop in parameters.GetType().GetProperties())
                {
                    command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(parameters) ?? DBNull.Value);
                }
            }

            using var reader = await command.ExecuteReaderAsync();
            table.Load(reader);
            return table;
        }

        public async Task<int> ExecuteSqlAsync(string sql, object? parameters = null)
        {
            var conStr = IDataAccessService.GetDbConStr(_config);
            using var connection = new SqlConnection(conStr);
            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);
            if (parameters != null)
            {
                foreach (var prop in parameters.GetType().GetProperties())
                {
                    command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(parameters) ?? DBNull.Value);
                }
            }

            return await command.ExecuteNonQueryAsync();
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public string CreateWhere(IEnumerable<string> conditions, string separator = " AND ")
        {
            var valid = conditions.Where(c => !string.IsNullOrWhiteSpace(c)).ToList();
            return valid.Count == 0 ? string.Empty : " WHERE " + string.Join(separator, valid);
        }

        public string AddWhere(string baseWhere, string condition, string separator = " AND ")
        {
            if (string.IsNullOrWhiteSpace(condition)) return baseWhere;
            if (string.IsNullOrWhiteSpace(baseWhere)) return " WHERE " + condition;
            return baseWhere + separator + condition;
        }
    }
}
