using BourbonAe.Core.Models.AEKB0040;
namespace BourbonAe.Core.Services.Features.AEKB0040
{
    public interface IAekb0040Service
    {
        Task<IReadOnlyList<Aekb0040Row>> SearchAsync(Aekb0040Filter filter, CancellationToken ct = default);
        Task<int> ApproveAsync(IEnumerable<string> applyNos, CancellationToken ct = default);
        Task<int> RejectAsync(IEnumerable<string> applyNos, CancellationToken ct = default);
    }
}
