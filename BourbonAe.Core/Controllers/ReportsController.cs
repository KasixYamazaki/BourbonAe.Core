using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BourbonAe.Core.Services.Reporting;
using BourbonAe.Core.Models.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BourbonAe.Core.Controllers
{
    //[Authorize]
    public class ReportsController : Controller
    {
        private readonly IReportExportService _export;
        public ReportsController(IReportExportService export) => _export = export;

        [HttpGet] public IActionResult Index() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportExcel()
        {
            var rows = new List<SampleRow>{
                new() { No=1, Code="A001", Name="りんご", Date=DateTime.Today.AddDays(-2), Quantity=10.5m, Amount=1200m, Status="処理済" },
                new() { No=2, Code="B010", Name="バナナ", Date=DateTime.Today.AddDays(-1), Quantity=5m, Amount=800m, Status="未処理" }
            };
            var cols = new ReportColumn<SampleRow>[]{
                new("No", x=>x.No, "0", 6, true),
                new("コード", x=>x.Code, null, 12),
                new("名称", x=>x.Name, null, 20),
                new("日付", x=>x.Date, "yyyy-mm-dd", 12),
                new("数量", x=>x.Quantity, "#,##0.00", 12, true),
                new("金額", x=>x.Amount, "#,##0", 12, true),
                new("状態", x=>x.Status, null, 10),
            };
            var bytes = await _export.CreateExcelAsync(rows, cols, "サンプル");
            var fileName = $"Sample_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportZip()
        {
            var rows = new List<SampleRow>{
                new() { No=1, Code="A001", Name="りんご", Date=DateTime.Today.AddDays(-2), Quantity=10.5m, Amount=1200m, Status="処理済" },
                new() { No=2, Code="B010", Name="バナナ", Date=DateTime.Today.AddDays(-1), Quantity=5m, Amount=800m, Status="未処理" }
            };
            var cols = new ReportColumn<SampleRow>[]{
                new("No", x=>x.No, "0", 6, true),
                new("コード", x=>x.Code, null, 12),
                new("名称", x=>x.Name, null, 20),
                new("日付", x=>x.Date, "yyyy-mm-dd", 12),
                new("数量", x=>x.Quantity, "#,##0.00", 12, true),
                new("金額", x=>x.Amount, "#,##0", 12, true),
                new("状態", x=>x.Status, null, 10),
            };
            var excel = await _export.CreateExcelAsync(rows, cols, "サンプル");
            var csv = "No,Code,Name,Date,Quantity,Amount,Status\r\n" +
                      string.Join("\r\n", rows.Select(r => $"{r.No},{r.Code},{r.Name},{r.Date:yyyy-MM-dd},{r.Quantity},{r.Amount},{r.Status}"));
            var csvBytes = System.Text.Encoding.UTF8.GetPreamble().Concat(System.Text.Encoding.UTF8.GetBytes(csv)).ToArray();
            var zipBytes = await _export.CreateZipAsync(("Sample.xlsx", excel), ("Sample.csv", csvBytes));
            var zipName = $"Sample_{DateTime.Now:yyyyMMddHHmmss}.zip";
            return File(zipBytes, "application/zip", zipName);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportPdf()
        {
            var rows = new List<SampleRow>{
                new() { No=1, Code="A001", Name="りんご", Date=DateTime.Today.AddDays(-2), Quantity=10.5m, Amount=1200m, Status="処理済" },
                new() { No=2, Code="B010", Name="バナナ", Date=DateTime.Today.AddDays(-1), Quantity=5m, Amount=800m, Status="未処理" }
            };
            var cols = new ReportColumn<SampleRow>[]{
                new("No", x=>x.No),
                new("コード", x=>x.Code),
                new("名称", x=>x.Name),
                new("日付", x=>x.Date.ToString("yyyy-MM-dd")),
                new("数量", x=>x.Quantity),
                new("金額", x=>x.Amount),
                new("状態", x=>x.Status),
            };
            var bytes = await _export.CreatePdfBasicAsync(rows, cols, "PDF サンプル");
            var fileName = $"Sample_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            return File(bytes, "application/pdf", fileName);
        }
    }
}
