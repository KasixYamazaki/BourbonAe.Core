using System.Data;
using ClosedXML.Excel;

namespace BourbonAe.Core.Services.Export
{
    public interface IExcelExporter
    {
        byte[] ExportDataTable(DataTable table, string sheetName = "Sheet1");
        byte[] ExportList<T>(IEnumerable<T> data, string sheetName = "Sheet1");
    }

    public sealed class ExcelExporter : IExcelExporter
    {
        public byte[] ExportDataTable(DataTable table, string sheetName = "Sheet1")
        {
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(table, sheetName);
            ws.Columns().AdjustToContents();
            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }

        public byte[] ExportList<T>(IEnumerable<T> data, string sheetName = "Sheet1")
        {
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(sheetName);
            var props = typeof(T).GetProperties();
            for (int i = 0; i < props.Length; i++)
                ws.Cell(1, i + 1).Value = props[i].Name;

            int row = 2;
            foreach (var item in data)
            {
                for (int col = 0; col < props.Length; col++)
                    ws.Cell(row, col + 1).Value = XLCellValue.FromObject(props[col].GetValue(item));
                row++;
            }
            ws.Columns().AdjustToContents();
            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }
    }
}
