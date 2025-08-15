using System.IO.Compression;

namespace BourbonAe.Core.Services.Compression
{
    public interface IZipService
    {
        Task CreateZipAsync(string zipPath, IEnumerable<(string entryName, Stream content)> entries, CancellationToken ct = default);
        Task ExtractZipAsync(string zipPath, string destinationDirectory, CancellationToken ct = default);
    }

    public sealed class ZipService : IZipService
    {
        public async Task CreateZipAsync(string zipPath, IEnumerable<(string entryName, Stream content)> entries, CancellationToken ct = default)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(zipPath)!);
            using var zipStream = new FileStream(zipPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            using var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: false);

            foreach (var (entryName, content) in entries)
            {
                ct.ThrowIfCancellationRequested();
                var entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
                await using var entryStream = entry.Open();
                await content.CopyToAsync(entryStream, ct);
            }
        }

        public async Task ExtractZipAsync(string zipPath, string destinationDirectory, CancellationToken ct = default)
        {
            await Task.Run(() =>
            {
                Directory.CreateDirectory(destinationDirectory);
                ZipFile.ExtractToDirectory(zipPath, destinationDirectory, overwriteFiles: true);
            }, ct);
        }
    }
}
