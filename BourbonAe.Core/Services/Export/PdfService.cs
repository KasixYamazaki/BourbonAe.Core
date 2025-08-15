using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BourbonAe.Core.Services.Export
{
    public interface IPdfService
    {
        byte[] GenerateSimplePdf(string title, IEnumerable<string> lines);
    }

    public sealed class PdfService : IPdfService
    {
        public byte[] GenerateSimplePdf(string title, IEnumerable<string> lines)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Header().Text(title).SemiBold().FontSize(18).AlignCenter();
                    page.Content().Column(col =>
                    {
                        foreach (var l in lines)
                            col.Item().Text(l);
                    });
                    page.Footer().AlignCenter().Text(x => x.CurrentPageNumber());
                });
            });
            using var ms = new MemoryStream();
            doc.GeneratePdf(ms);
            return ms.ToArray();
        }
    }
}
