using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Models.AESJ1110
{
    public class Aesj1110Row
    {
        [Display(Name = "伝票No.")] public string SlipNo { get; set; } = string.Empty;
        [Display(Name = "得意先コード")] public string CustomerCode { get; set; } = string.Empty;
        [Display(Name = "得意先名")] public string CustomerName { get; set; } = string.Empty;
        [Display(Name = "日付"), DataType(DataType.Date)] public DateTime Date { get; set; }
        [Display(Name = "状態")] public string Status { get; set; } = string.Empty;
        [Display(Name = "金額"), DataType(DataType.Currency)] public decimal Amount { get; set; }
    }
}
