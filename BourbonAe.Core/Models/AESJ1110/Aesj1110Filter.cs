using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Models.AESJ1110
{
    public class Aesj1110Filter
    {
        [Display(Name = "得意先コード")]
        public string? CustomerCode { get; set; }
        [Display(Name = "期間（開始）"), DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }
        [Display(Name = "期間（終了）"), DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }
        [Display(Name = "状態")]
        public string? Status { get; set; }
    }
}
