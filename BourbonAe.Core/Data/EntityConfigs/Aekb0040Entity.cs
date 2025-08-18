using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BourbonAe.Core.Data.Entities
{
    public class Aekb0040Entity
    {
        [Key] public string ApplyNo { get; set; } = null!;
        public string DeptCode { get; set; } = null!;
        public string Applicant { get; set; } = null!;
        public DateTime ApplyDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "未承認";
    }
    public static class Aekb0040EntityConfig
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Aekb0040Entity>();
            e.ToTable("AEKB0040"); // TODO: actual table
            e.HasKey(x => x.ApplyNo);
            e.Property(x => x.ApplyNo).HasColumnName("APPLY_NO").HasMaxLength(30);
            e.Property(x => x.DeptCode).HasColumnName("DEPT_CD").HasMaxLength(20);
            e.Property(x => x.Applicant).HasColumnName("APPLICANT").HasMaxLength(100);
            e.Property(x => x.ApplyDate).HasColumnName("APPLY_DATE");
            e.Property(x => x.Amount).HasColumnName("AMOUNT").HasPrecision(18,2);
            e.Property(x => x.Status).HasColumnName("STATUS").HasMaxLength(20);
        }
    }
}
