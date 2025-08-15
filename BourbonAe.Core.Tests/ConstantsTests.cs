using BourbonAe.Core.Models.Constants;
using Xunit;

namespace BourbonAe.Core.Tests
{
    /// <summary>
    /// Unit tests verifying that constant values in the migrated C# code match
    /// the values defined in the original VB project.  These tests use xUnit
    /// and ensure that the constants remain stable across refactoring.
    /// </summary>
    public class ConstantsTests
    {
        [Fact]
        public void ExportTypeValues_ShouldMatchExpected()
        {
            Assert.Equal("1", ExportType.Pdf.Code);
            Assert.Equal("帳票（PDF）", ExportType.Pdf.Name);
            Assert.Equal("2", ExportType.Excel.Code);
            Assert.Equal("データ（EXCEL）", ExportType.Excel.Name);
        }

        [Fact]
        public void ExportDataTypeValues_ShouldMatchExpected()
        {
            Assert.Equal("0", ExportDataType.TokuyakuAll.Code);
            Assert.Equal("特約・登録店（全て）", ExportDataType.TokuyakuAll.Name);
            Assert.Equal("1", ExportDataType.TokuyakuConfirmed.Code);
            Assert.Equal("特約・登録店（確定）", ExportDataType.TokuyakuConfirmed.Name);
            Assert.Equal("2", ExportDataType.TokuyakuUnconfirmed.Code);
            Assert.Equal("特約・登録店（未確定）", ExportDataType.TokuyakuUnconfirmed.Name);
            Assert.Equal("3", ExportDataType.MannequinExpense.Code);
            Assert.Equal("マネキン販促費", ExportDataType.MannequinExpense.Name);
        }

        [Fact]
        public void AppDefValues_ShouldMatchExpected()
        {
            Assert.Equal("010", AppDef.CompanyCode);
            Assert.Equal("ブルボン", AppDef.CompanyName);
            Assert.Equal(-2, AppDef.CarryoverMonths);
            Assert.Equal("**** 指定無 ****", AppDef.NotSpecified);
        }
    }
}