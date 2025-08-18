using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Data.Entities
{
    public class Aemm0010Entity
    {
        [Key] public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
    public static class Aemm0010EntityConfig
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Aemm0010Entity>();
            e.ToTable("AEMM0010"); // TODO: actual table
            e.HasKey(x => x.Code);
            e.Property(x => x.Code).HasColumnName("CODE").HasMaxLength(30);
            e.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(100);
            e.Property(x => x.IsActive).HasColumnName("IS_ACTIVE");
        }
    }
}
