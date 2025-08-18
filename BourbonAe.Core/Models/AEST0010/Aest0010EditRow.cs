namespace BourbonAe.Core.Models.AEST0010
{
    public class Aest0010EditRow
    {
        public int? Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.Today;

        public string Status { get; set; } = "未処理";

        public string? Remarks { get; set; }
    }
}
