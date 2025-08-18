using System.Collections.Generic;
namespace BourbonAe.Core.Models.AEST0020
{
    public class Aest0020PageViewModel
    {
        public Aest0020Filter Filter { get; set; } = new();
        public IReadOnlyList<Aest0020Row> Rows { get; set; } = new List<Aest0020Row>();
        public Aest0020EditRow Edit { get; set; } = new();
        public List<int> SelectedIds { get; set; } = new();
    }
}
