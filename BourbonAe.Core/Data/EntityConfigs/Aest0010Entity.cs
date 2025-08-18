using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BourbonAe.Core.Data.Entities
{
    public class Aest0010Entity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Status { get; set; } = "未処理";

        public string? Remarks { get; set; }
    }

    public static class Aest0010EntityConfig
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Aest0010Entity>();

            e.ToTable("AEST0010");
            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("ID");

            e.Property(x => x.Title)
                .HasColumnName("TITLE")
                .HasMaxLength(200);

            e.Property(x => x.Date)
                .HasColumnName("DT");

            e.Property(x => x.Status)
                .HasColumnName("STATUS")
                .HasMaxLength(20);

            e.Property(x => x.Remarks)
                .HasColumnName("REMARKS")
                .HasMaxLength(4000);
        }
    }
}
