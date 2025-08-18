using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Models.AEST0020
{
    public class Aest0020EditRow
    {
        public int? Id { get; set; }

        [Required, Display(Name = "コード")]
        public string Code { get; set; } = string.Empty;

        [Required, Display(Name = "名称")]
        public string Name { get; set; } = string.Empty;

        [Required, DataType(DataType.Date), Display(Name = "日付")]
        public DateTime Date { get; set; } = DateTime.Today;

        [Range(0, double.MaxValue), Display(Name = "数量")]
        public decimal Quantity { get; set; }

        [Range(0, double.MaxValue), Display(Name = "金額")]
        public decimal Amount { get; set; }

        [Required, Display(Name = "状態")]
        public string Status { get; set; } = "未処理";
    }
}
