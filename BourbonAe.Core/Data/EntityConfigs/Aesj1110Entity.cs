using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Data.Entities
{
    public class Aesj1110Entity
    {
        [Key] public string SlipNo { get; set; } = null!;
        public string CustomerCode { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Status { get; set; } = null!;
        public decimal Amount { get; set; }
    }
    public static class Aesj1110EntityConfig
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Aesj1110Entity>();
            e.ToTable("AESJ1110"); // TODO: use actual table name
            e.HasKey(x => x.SlipNo);
            e.Property(x => x.SlipNo).HasColumnName("SLIP_NO").HasMaxLength(30);
            e.Property(x => x.CustomerCode).HasColumnName("CUSTOMER_CD").HasMaxLength(20);
            e.Property(x => x.CustomerName).HasColumnName("CUSTOMER_NM").HasMaxLength(100);
            e.Property(x => x.Date).HasColumnName("SLIP_DATE");
            e.Property(x => x.Status).HasColumnName("STATUS").HasMaxLength(20);
            e.Property(x => x.Amount).HasColumnName("AMOUNT").HasPrecision(18,2);
        }
    }
}
