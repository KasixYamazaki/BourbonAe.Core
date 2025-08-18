using Microsoft.EntityFrameworkCore;
using BourbonAe.Core.Models.AEMM0010;
using BourbonAe.Core.Data.Entities;
namespace BourbonAe.Core.Services.Features.AEMM0010
{
    public sealed class Aemm0010Service : IAemm0010Service
    {
        private readonly DbContext _db;
        public Aemm0010Service(DbContext db) => _db = db;

        public async Task<List<Aemm0010EditRow>> SearchAsync(Aemm0010Filter filter, CancellationToken ct = default)
        {
            var q = _db.Set<Aemm0010Entity>().AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter.Code)) q = q.Where(x => x.Code.Contains(filter.Code));
            if (!string.IsNullOrWhiteSpace(filter.Name)) q = q.Where(x => x.Name.Contains(filter.Name));
            if (filter.ActiveOnly) q = q.Where(x => x.IsActive);
            return await q.OrderBy(x => x.Code).Select(x => new Aemm0010EditRow
            { Code = x.Code, Name = x.Name, IsActive = x.IsActive }).ToListAsync(ct);
        }

        public async Task<Aemm0010EditRow?> FindAsync(string code, CancellationToken ct = default)
        {
            var e = await _db.Set<Aemm0010Entity>().FindAsync(new object?[] { code }, ct);
            if (e is null) return null;
            return new Aemm0010EditRow { Code = e.Code, Name = e.Name, IsActive = e.IsActive };
        }

        public async Task<int> UpsertAsync(Aemm0010EditRow row, CancellationToken ct = default)
        {
            var set = _db.Set<Aemm0010Entity>();
            var e = await set.FindAsync(new object?[] { row.Code }, ct);
            if (e is null)
            {
                e = new Aemm0010Entity { Code = row.Code, Name = row.Name, IsActive = row.IsActive };
                await set.AddAsync(e, ct);
            }
            else
            {
                e.Name = row.Name;
                e.IsActive = row.IsActive;
            }
            return await _db.SaveChangesAsync(ct);
        }

        public async Task<int> DeleteAsync(string code, CancellationToken ct = default)
        {
            var set = _db.Set<Aemm0010Entity>();
            var e = await set.FindAsync(new object?[] { code }, ct);
            if (e is null) return 0;
            set.Remove(e);
            return await _db.SaveChangesAsync(ct);
        }
    }
}
