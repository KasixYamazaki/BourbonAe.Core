using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BourbonAe.Core.Data.Entities
{
    // Replace table/columns according to the actual schema
    public class Aest0020Entity
    {
        [Key] public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "未処理";
    }

    public static class Aest0020EntityConfig
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Aest0020Entity>();
            e.ToTable("AEST0020"); // TODO: actual table name
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("ID");
            e.Property(x => x.Code).HasColumnName("CODE").HasMaxLength(50);
            e.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(200);
            e.Property(x => x.Date).HasColumnName("DT");
            e.Property(x => x.Quantity).HasColumnName("QTY").HasPrecision(18, 2);
            e.Property(x => x.Amount).HasColumnName("AMOUNT").HasPrecision(18, 2);
            e.Property(x => x.Status).HasColumnName("STATUS").HasMaxLength(20);
        }
    }
}
