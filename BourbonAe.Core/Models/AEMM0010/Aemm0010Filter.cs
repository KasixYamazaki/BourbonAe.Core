using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Models.AEMM0010
{
    public class Aemm0010Filter
    {
        [Display(Name = "コード")] public string? Code { get; set; }
        [Display(Name = "名称")] public string? Name { get; set; }
        [Display(Name = "有効のみ")] public bool ActiveOnly { get; set; } = true;
    }
}
