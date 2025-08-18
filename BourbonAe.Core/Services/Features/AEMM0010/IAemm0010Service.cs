using BourbonAe.Core.Models.AEMM0010;
namespace BourbonAe.Core.Services.Features.AEMM0010
{
    public interface IAemm0010Service
    {
        Task<List<Aemm0010EditRow>> SearchAsync(Aemm0010Filter filter, CancellationToken ct = default);
        Task<int> UpsertAsync(Aemm0010EditRow row, CancellationToken ct = default);
        Task<int> DeleteAsync(string code, CancellationToken ct = default);
        Task<Aemm0010EditRow?> FindAsync(string code, CancellationToken ct = default);
    }
}
