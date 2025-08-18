using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Models.AEKB0040
{
    public class Aekb0040Filter
    {
        [Display(Name = "部門コード")] public string? DeptCode { get; set; }
        [Display(Name = "申請者")] public string? Applicant { get; set; }
        [Display(Name = "状態")] public string? Status { get; set; }
        [Display(Name = "日付（開始）"), DataType(DataType.Date)] public DateTime? FromDate { get; set; }
        [Display(Name = "日付（終了）"), DataType(DataType.Date)] public DateTime? ToDate { get; set; }
    }
}
