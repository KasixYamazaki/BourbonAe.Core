using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Models.AEST0020
{
    public class Aest0020Row
    {
        [Display(Name = "ID")] public int Id { get; set; }
        [Display(Name = "コード")] public string Code { get; set; } = string.Empty;
        [Display(Name = "名称")] public string Name { get; set; } = string.Empty;
        [Display(Name = "日付"), DataType(DataType.Date)] public DateTime Date { get; set; }
        [Display(Name = "数量")] public decimal Quantity { get; set; }
        [Display(Name = "金額"), DataType(DataType.Currency)] public decimal Amount { get; set; }
        [Display(Name = "状態")] public string Status { get; set; } = "未処理";
    }
}
