using System.Collections.Generic;

namespace BourbonAe.Core.Models.AEST0010
{
    public class Aest0010PageViewModel
    {
        public Aest0010Filter Filter { get; set; } = new();

        public IReadOnlyList<Aest0010Row> Rows { get; set; } = new List<Aest0010Row>();

        public Aest0010EditRow Edit { get; set; } = new();

        public List<int> SelectedIds { get; set; } = new();
    }
}
