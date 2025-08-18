using BourbonAe.Core.Models.AEST0020;

namespace BourbonAe.Core.Services.Features.AEST0020
{
    public interface IAest0020Service
    {
        Task<IReadOnlyList<Aest0020Row>> SearchAsync(Aest0020Filter filter, CancellationToken ct = default);
        Task<Aest0020EditRow?> FindAsync(int id, CancellationToken ct = default);
        Task<int> UpsertAsync(Aest0020EditRow row, CancellationToken ct = default);
        Task<int> DeleteAsync(int id, CancellationToken ct = default);
        Task<int> BulkUpdateStatusAsync(IEnumerable<int> ids, string newStatus, CancellationToken ct = default);
    }
}
