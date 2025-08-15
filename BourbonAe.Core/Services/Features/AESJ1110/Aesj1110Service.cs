using Microsoft.EntityFrameworkCore;
using BourbonAe.Core.Models.AESJ1110;
using BourbonAe.Core.Data;
using BourbonAe.Core.Data.Entities;
namespace BourbonAe.Core.Services.Features.AESJ1110
{
    public sealed class Aesj1110Service : IAesj1110Service
    {
        private readonly ApplicationDbContext _db;
        public Aesj1110Service(ApplicationDbContext db) => _db = db;
        public async Task<IReadOnlyList<Aesj1110Row>> SearchAsync(Aesj1110Filter filter, CancellationToken ct = default)
        {
            IQueryable<Aesj1110Entity> q = _db.Set<Aesj1110Entity>().AsNoTracking();
            if (!string.IsNullOrWhiteSpace(filter.CustomerCode)) q = q.Where(x => x.CustomerCode == filter.CustomerCode);
            if (filter.FromDate.HasValue) q = q.Where(x => x.Date >= filter.FromDate.Value);
            if (filter.ToDate.HasValue) q = q.Where(x => x.Date <= filter.ToDate.Value);
            if (!string.IsNullOrWhiteSpace(filter.Status)) q = q.Where(x => x.Status == filter.Status);
            return await q.OrderByDescending(x => x.Date).ThenBy(x => x.SlipNo)
                .Select(x => new Aesj1110Row
                { SlipNo = x.SlipNo, CustomerCode = x.CustomerCode, CustomerName = x.CustomerName, Date = x.Date, Status = x.Status, Amount = x.Amount })
                .ToListAsync(ct);
        }
        public async Task<int> SaveAsync(IEnumerable<string> slipNos, CancellationToken ct = default)
        {
            var set = _db.Set<Aesj1110Entity>();
            var targets = await set.Where(x => slipNos.Contains(x.SlipNo)).ToListAsync(ct);
            foreach (var t in targets) { t.Status = "確定"; } // TODO: adjust
            return await _db.SaveChangesAsync(ct);
        }
    }
}
