using BourbonAe.Core.Data.Entities;
using BourbonAe.Core.Models.Entities;
using BourbonAe.Core.Services.Auth;
using Microsoft.EntityFrameworkCore;

namespace BourbonAe.Core.Data;

public class ApplicationDbContext : DbContext, IAppDb
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Tokui> Tokuis { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Aesj1110Entity> Aesj1110Entities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tokui>().ToTable("M_TOKUI");
        modelBuilder.Entity<Tokui>().HasKey(t => t.TokuiCd);
        modelBuilder.Entity<Tokui>().Property(t => t.TokuiCd).HasColumnName("KY_TOKUI_CD");
        modelBuilder.Entity<Tokui>().Property(t => t.TokuiNm).HasColumnName("TOKUI_NM");
        modelBuilder.Entity<Tokui>().Property(t => t.TokuiKana).HasColumnName("TOKUI_KANA");

        Aesj1110EntityConfig.Map(modelBuilder);
    }
}

