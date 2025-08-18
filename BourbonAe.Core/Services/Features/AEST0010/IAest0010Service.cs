using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BourbonAe.Core.Models.AEST0010;

namespace BourbonAe.Core.Services.Features.AEST0010
{
    public interface IAest0010Service
    {
        Task<IReadOnlyList<Aest0010Row>> SearchAsync(
            Aest0010Filter filter,
            CancellationToken ct = default);

        Task<Aest0010EditRow?> FindAsync(
            int id,
            CancellationToken ct = default);

        Task<int> UpsertAsync(
            Aest0010EditRow row,
            CancellationToken ct = default);

        Task<int> DeleteAsync(
            int id,
            CancellationToken ct = default);
    }
}
