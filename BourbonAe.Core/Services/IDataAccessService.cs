using System.Data;

namespace BourbonAe.Core.Services
{
    public interface IDataAccessService
    {
        static string GetDbConStr(IConfiguration config) =>
            config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("接続文字列が設定されていません。");

        Task<List<T>> QueryAsync<T>(Func<IQueryable<T>, IQueryable<T>>? filter = null) where T : class;
        Task<DataTable> RawQueryAsync(string sql, object? parameters = null);
        Task<int> ExecuteSqlAsync(string sql, object? parameters = null);
        Task ExecuteInTransactionAsync(Func<Task> action);

        string CreateWhere(IEnumerable<string> conditions, string separator = " AND ");
        string AddWhere(string baseWhere, string condition, string separator = " AND ");
    }
}
