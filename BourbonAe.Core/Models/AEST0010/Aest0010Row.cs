namespace BourbonAe.Core.Models.AEST0010
{
    public class Aest0010Row
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? Remarks { get; set; }
    }
}
