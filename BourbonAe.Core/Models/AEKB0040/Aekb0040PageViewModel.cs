namespace BourbonAe.Core.Models.AEKB0040
{
    public class Aekb0040PageViewModel
    {
        public Aekb0040Filter Filter { get; set; } = new();
        public IReadOnlyList<Aekb0040Row> Rows { get; set; } = new List<Aekb0040Row>();
        public List<string> SelectedApplyNos { get; set; } = new();
    }
}
