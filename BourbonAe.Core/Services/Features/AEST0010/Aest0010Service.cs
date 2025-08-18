using Microsoft.EntityFrameworkCore;
using BourbonAe.Core.Models.AEST0010;
using BourbonAe.Core.Data.Entities;

namespace BourbonAe.Core.Services.Features.AEST0010
{
    public sealed class Aest0010Service : IAest0010Service
    {
        private readonly DbContext _db;

        public Aest0010Service(DbContext db) => _db = db;

        public async Task<IReadOnlyList<Aest0010Row>> SearchAsync(
            Aest0010Filter f,
            CancellationToken ct = default)
        {
            var q = _db.Set<Aest0010Entity>()
                       .AsNoTracking()
                       .AsQueryable();

            if (!string.IsNullOrWhiteSpace(f.Keyword))
            {
                q = q.Where(x =>
                    x.Title.Contains(f.Keyword) ||
                    (x.Remarks ?? "").Contains(f.Keyword));
            }

            if (f.FromDate.HasValue)
            {
                q = q.Where(x => x.Date >= f.FromDate.Value);
            }

            if (f.ToDate.HasValue)
            {
                q = q.Where(x => x.Date <= f.ToDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(f.Status))
            {
                q = q.Where(x => x.Status == f.Status);
            }

            return await q.OrderByDescending(x => x.Date)
                          .ThenBy(x => x.Id)
                          .Select(x => new Aest0010Row
                          {
                              Id = x.Id,
                              Title = x.Title,
                              Date = x.Date,
                              Status = x.Status,
                              Remarks = x.Remarks
                          })
                          .ToListAsync(ct);
        }

        public async Task<Aest0010EditRow?> FindAsync(
            int id,
            CancellationToken ct = default)
        {
            var e = await _db.Set<Aest0010Entity>()
                             .FindAsync(new object?[] { id }, ct);

            return e is null
                ? null
                : new Aest0010EditRow
                {
                    Id = e.Id,
                    Title = e.Title,
                    Date = e.Date,
                    Status = e.Status,
                    Remarks = e.Remarks
                };
        }

        public async Task<int> UpsertAsync(
            Aest0010EditRow row,
            CancellationToken ct = default)
        {
            var set = _db.Set<Aest0010Entity>();
            Aest0010Entity e;

            if (row.Id is null)
            {
                e = new Aest0010Entity
                {
                    Title = row.Title,
                    Date = row.Date,
                    Status = row.Status,
                    Remarks = row.Remarks
                };

                await set.AddAsync(e, ct);
            }
            else
            {
                e = await set.FindAsync(new object?[] { row.Id.Value }, ct)
                    ?? new Aest0010Entity();

                e.Title = row.Title;
                e.Date = row.Date;
                e.Status = row.Status;
                e.Remarks = row.Remarks;

                if (e.Id == 0)
                {
                    await set.AddAsync(e, ct);
                }
            }

            return await _db.SaveChangesAsync(ct);
        }

        public async Task<int> DeleteAsync(
            int id,
            CancellationToken ct = default)
        {
            var set = _db.Set<Aest0010Entity>();
            var e = await set.FindAsync(new object?[] { id }, ct);

            if (e is null)
                return 0;

            set.Remove(e);

            return await _db.SaveChangesAsync(ct);
        }
    }
}
