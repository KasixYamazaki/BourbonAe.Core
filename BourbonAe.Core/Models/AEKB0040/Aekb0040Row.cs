using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Models.AEKB0040
{
    public class Aekb0040Row
    {
        [Display(Name = "申請No.")] public string ApplyNo { get; set; } = string.Empty;
        [Display(Name = "部門コード")] public string DeptCode { get; set; } = string.Empty;
        [Display(Name = "申請者")] public string Applicant { get; set; } = string.Empty;
        [Display(Name = "日付"), DataType(DataType.Date)] public DateTime ApplyDate { get; set; }
        [Display(Name = "金額"), DataType(DataType.Currency)] public decimal Amount { get; set; }
        [Display(Name = "状態")] public string Status { get; set; } = "未承認";
    }
}
