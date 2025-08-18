using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Models.AESJ1110
{
    public class Aesj1110PageViewModel
    {
        public Aesj1110Filter Filter { get; set; } = new();
        [Display(Name = "検索結果")] public IReadOnlyList<Aesj1110Row> Rows { get; set; } = new List<Aesj1110Row>();
        public List<string> SelectedSlipNos { get; set; } = new();
    }
}
