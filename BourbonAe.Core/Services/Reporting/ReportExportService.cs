using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace BourbonAe.Core.Services.Reporting
{
    public sealed class ReportExportService : IReportExportService
    {
        public async Task<byte[]> CreateExcelAsync<T>(IReadOnlyList<T> rows, ReportColumn<T>[] columns, string title, CancellationToken ct = default)
        {
            using var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Sheet1");
            ws.Cell(1,1).Value = title;
            ws.Range(1,1,1,columns.Length).Merge().Style.Font.SetBold().Font.SetFontSize(14).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            for (int c = 0; c < columns.Length; c++) ws.Cell(2, c + 1).Value = columns[c].Header;
            var header = ws.Range(2,1,2,columns.Length);
            header.Style.Font.SetBold(); header.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#f1f3f5"));
            header.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            header.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).SetInsideBorder(XLBorderStyleValues.Thin);
            for (int r = 0; r < rows.Count; r++)
            {
                var row = rows[r];
                for (int c = 0; c < columns.Length; c++)
                {
                    var col = columns[c];
                    var cell = ws.Cell(r + 3, c + 1);
                    var val = col.Selector(row);
                    cell.Value = val ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(col.NumberFormat)) cell.Style.NumberFormat.Format = col.NumberFormat;
                    if (col.RightAlign) cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                }
            }
            if (rows.Count > 0) ws.Range(3,1,rows.Count+2,columns.Length).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).SetInsideBorder(XLBorderStyleValues.Hair);
            for (int c = 0; c < columns.Length; c++){ if (columns[c].Width is double w) ws.Column(c+1).Width = w; else ws.Column(c+1).AdjustToContents(); }
            ws.SheetView.FreezeRows(2); ws.RangeUsed()?.SetAutoFilter();
            var lastRow = rows.Count + 3;
            ws.Cell(lastRow, 1).Value = $"出力日時：{DateTime.Now:yyyy-MM-dd HH:mm}";
            ws.Range(lastRow,1,lastRow,Math.Max(1,columns.Length)).Merge().Style.Font.SetFontSize(9).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            using var ms = new MemoryStream(); wb.SaveAs(ms); return await Task.FromResult(ms.ToArray());
        }

        public async Task<byte[]> CreateZipAsync(params (string FileName, byte[] Content)[] entries)
        {
            using var ms = new MemoryStream();
            using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
            {
                foreach (var (FileName, Content) in entries)
                {
                    var e = zip.CreateEntry(FileName, CompressionLevel.Fastest);
                    using var zs = e.Open(); zs.Write(Content, 0, Content.Length);
                }
            }
            return await Task.FromResult(ms.ToArray());
        }

        public async Task<byte[]> CreatePdfBasicAsync<T>(IReadOnlyList<T> rows, ReportColumn<T>[] columns, string title)
        {
            var doc = new PdfDocument();
            var page = doc.AddPage(); page.Size = PdfSharpCore.PageSize.A4;
            var gfx = XGraphics.FromPdfPage(page);
            var fontTitle = new XFont("MS Gothic", 14, XFontStyle.Bold);
            var fontHeader = new XFont("MS Gothic", 10, XFontStyle.Bold);
            var fontCell = new XFont("MS Gothic", 10);
            double x=40, y=40, rowH=18, colW=(page.Width-80)/Math.Max(1, columns.Length);
            gfx.DrawString(title, fontTitle, XBrushes.Black, new XRect(0,y,page.Width,rowH), XStringFormats.TopCenter); y+=rowH*1.5;
            for (int c=0;c<columns.Length;c++){ gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, x+c*colW,y,colW,rowH); gfx.DrawString(columns[c].Header, fontHeader, XBrushes.Black, new XRect(x+c*colW+4,y+2,colW-8,rowH-4), XStringFormats.TopLeft); }
            y+=rowH;
            foreach (var r in rows)
            {
                for (int c=0;c<columns.Length;c++){ var text = Convert.ToString(columns[c].Selector(r)) ?? ""; gfx.DrawRectangle(XPens.Black, XBrushes.White, x+c*colW,y,colW,rowH); gfx.DrawString(text, fontCell, XBrushes.Black, new XRect(x+c*colW+4,y+2,colW-8,rowH-4), XStringFormats.TopLeft); }
                y+=rowH; if (y+rowH>page.Height-40){ page=doc.AddPage(); page.Size=PdfSharpCore.PageSize.A4; gfx=XGraphics.FromPdfPage(page); y=40; }
            }
            using var ms = new MemoryStream(); doc.Save(ms, false); return await Task.FromResult(ms.ToArray());
        }
    }
}
