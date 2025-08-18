namespace BourbonAe.Core.Models.AEMM0010
{
    public class Aemm0010PageViewModel
    {
        public Aemm0010Filter Filter { get; set; } = new();
        public List<Aemm0010EditRow> Rows { get; set; } = new();
        public Aemm0010EditRow Edit { get; set; } = new();
    }
}
