using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Models.AEST0020
{
    public class Aest0020Filter
    {
        [Display(Name = "コード/キーワード")]
        public string? Keyword { get; set; }

        [Display(Name = "開始日"), DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Display(Name = "終了日"), DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        [Display(Name = "状態")]
        public string? Status { get; set; }
    }
}
