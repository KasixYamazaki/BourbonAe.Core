using BourbonAe.Core.Models.AESJ1110;
namespace BourbonAe.Core.Services.Features.AESJ1110
{
    public interface IAesj1110Service
    {
        Task<IReadOnlyList<Aesj1110Row>> SearchAsync(Aesj1110Filter filter, CancellationToken ct = default);
        Task<int> SaveAsync(IEnumerable<string> slipNos, CancellationToken ct = default);
    }
}
