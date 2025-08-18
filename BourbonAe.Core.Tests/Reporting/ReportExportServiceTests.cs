using System.Collections.Generic;
using System.Threading.Tasks;
using BourbonAe.Core.Models.Reporting;
using BourbonAe.Core.Services.Reporting;
using Xunit;

namespace BourbonAe.Core.Tests.Reporting
{
    public class ReportExportServiceTests
    {
        [Fact]
        public async Task CreateExcel_WritesWorkbookBytes()
        {
            var svc = new ReportExportService();
            var rows = new List<SampleRow>
            {
                new()
                {
                    No = 1,
                    Code = "A001",
                    Name = "りんご",
                    Quantity = 10,
                    Amount = 1000
                }
            };
            var cols = new ReportColumn<SampleRow>[]
            {
                new("No", x => x.No),
                new("コード", x => x.Code),
                new("名称", x => x.Name),
                new("数量", x => x.Quantity, "#,##0.00", 12, true),
                new("金額", x => x.Amount, "#,##0", 12, true),
            };
            var bytes = await svc.CreateExcelAsync(rows, cols, "テスト");
            Assert.NotNull(bytes);
            Assert.True(bytes.Length > 0);
        }
    }
}
