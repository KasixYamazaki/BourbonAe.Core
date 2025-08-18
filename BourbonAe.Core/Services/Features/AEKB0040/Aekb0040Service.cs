using Microsoft.EntityFrameworkCore;
using BourbonAe.Core.Models.AEKB0040;
using BourbonAe.Core.Data.Entities;
namespace BourbonAe.Core.Services.Features.AEKB0040
{
    public sealed class Aekb0040Service : IAekb0040Service
    {
        private readonly DbContext _db;
        public Aekb0040Service(DbContext db) => _db = db;

        public async Task<IReadOnlyList<Aekb0040Row>> SearchAsync(Aekb0040Filter filter, CancellationToken ct = default)
        {
            var q = _db.Set<Aekb0040Entity>().AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter.DeptCode)) q = q.Where(x => x.DeptCode == filter.DeptCode);
            if (!string.IsNullOrWhiteSpace(filter.Applicant)) q = q.Where(x => x.Applicant.Contains(filter.Applicant));
            if (filter.FromDate.HasValue) q = q.Where(x => x.ApplyDate >= filter.FromDate.Value);
            if (filter.ToDate.HasValue) q = q.Where(x => x.ApplyDate <= filter.ToDate.Value);
            if (!string.IsNullOrWhiteSpace(filter.Status)) q = q.Where(x => x.Status == filter.Status);
            return await q.OrderByDescending(x => x.ApplyDate).ThenBy(x => x.ApplyNo)
                .Select(x => new Aekb0040Row { ApplyNo = x.ApplyNo, DeptCode = x.DeptCode, Applicant = x.Applicant, ApplyDate = x.ApplyDate, Amount = x.Amount, Status = x.Status })
                .ToListAsync(ct);
        }

        public async Task<int> ApproveAsync(IEnumerable<string> applyNos, CancellationToken ct = default)
        {
            var set = _db.Set<Aekb0040Entity>();
            var targets = await set.Where(x => applyNos.Contains(x.ApplyNo)).ToListAsync(ct);
            foreach (var t in targets) t.Status = "承認";
            return await _db.SaveChangesAsync(ct);
        }

        public async Task<int> RejectAsync(IEnumerable<string> applyNos, CancellationToken ct = default)
        {
            var set = _db.Set<Aekb0040Entity>();
            var targets = await set.Where(x => applyNos.Contains(x.ApplyNo)).ToListAsync(ct);
            foreach (var t in targets) t.Status = "却下";
            return await _db.SaveChangesAsync(ct);
        }
    }
}
