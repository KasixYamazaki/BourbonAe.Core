using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Models.AEMM0010
{
    public class Aemm0010EditRow
    {
        [Required, Display(Name = "コード")] public string Code { get; set; } = string.Empty;
        [Required, Display(Name = "名称")] public string Name { get; set; } = string.Empty;
        [Display(Name = "有効")] public bool IsActive { get; set; } = true;
    }
}
