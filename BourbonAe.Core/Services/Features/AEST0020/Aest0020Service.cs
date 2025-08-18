using Microsoft.EntityFrameworkCore;
using BourbonAe.Core.Models.AEST0020;
using BourbonAe.Core.Data.Entities;

namespace BourbonAe.Core.Services.Features.AEST0020
{
    public sealed class Aest0020Service : IAest0020Service
    {
        private readonly DbContext _db;
        public Aest0020Service(DbContext db) => _db = db;

        public async Task<IReadOnlyList<Aest0020Row>> SearchAsync(Aest0020Filter filter, CancellationToken ct = default)
        {
            var q = _db.Set<Aest0020Entity>().AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
                q = q.Where(x => x.Code.Contains(filter.Keyword) || x.Name.Contains(filter.Keyword));

            if (filter.FromDate.HasValue)
                q = q.Where(x => x.Date >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                q = q.Where(x => x.Date <= filter.ToDate.Value);

            if (!string.IsNullOrWhiteSpace(filter.Status))
                q = q.Where(x => x.Status == filter.Status);

            return await q.OrderByDescending(x => x.Date).ThenBy(x => x.Code)
                .Select(x => new Aest0020Row
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Date = x.Date,
                    Quantity = x.Quantity,
                    Amount = x.Amount,
                    Status = x.Status
                })
                .ToListAsync(ct);
        }

        public async Task<Aest0020EditRow?> FindAsync(int id, CancellationToken ct = default)
        {
            var e = await _db.Set<Aest0020Entity>().FindAsync(new object?[] { id }, ct);
            if (e is null) return null;
            return new Aest0020EditRow { Id = e.Id, Code = e.Code, Name = e.Name, Date = e.Date, Quantity = e.Quantity, Amount = e.Amount, Status = e.Status };
        }

        public async Task<int> UpsertAsync(Aest0020EditRow row, CancellationToken ct = default)
        {
            var set = _db.Set<Aest0020Entity>();
            Aest0020Entity e;
            if (row.Id is null)
            {
                e = new Aest0020Entity { Code = row.Code, Name = row.Name, Date = row.Date, Quantity = row.Quantity, Amount = row.Amount, Status = row.Status };
                await set.AddAsync(e, ct);
            }
            else
            {
                e = await set.FindAsync(new object?[] { row.Id.Value }, ct) ?? new Aest0020Entity();
                e.Code = row.Code;
                e.Name = row.Name;
                e.Date = row.Date;
                e.Quantity = row.Quantity;
                e.Amount = row.Amount;
                e.Status = row.Status;
                if (e.Id == 0) await set.AddAsync(e, ct);
            }
            return await _db.SaveChangesAsync(ct);
        }

        public async Task<int> DeleteAsync(int id, CancellationToken ct = default)
        {
            var set = _db.Set<Aest0020Entity>();
            var e = await set.FindAsync(new object?[] { id }, ct);
            if (e is null) return 0;
            set.Remove(e);
            return await _db.SaveChangesAsync(ct);
        }

        public async Task<int> BulkUpdateStatusAsync(IEnumerable<int> ids, string newStatus, CancellationToken ct = default)
        {
            var set = _db.Set<Aest0020Entity>();
            var targets = await set.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
            foreach (var t in targets) t.Status = newStatus;
            return await _db.SaveChangesAsync(ct);
        }
    }
}
