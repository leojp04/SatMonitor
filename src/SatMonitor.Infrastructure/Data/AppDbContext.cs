using Microsoft.EntityFrameworkCore;
using SatMonitor.Domain.Entities;

namespace SatMonitor.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Missao> Missoes { get; set; }
    public DbSet<Satelite> Satelites { get; set; }
    public DbSet<Sensor> Sensores { get; set; }
    public DbSet<LeituraSensor> Leituras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Missao>(entity =>
        {
            entity.ToTable("MISSOES");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).HasMaxLength(500);
            entity.Property(e => e.Status).HasConversion<string>();
        });

        modelBuilder.Entity<Satelite>(entity =>
        {
            entity.ToTable("SATELITES");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.Missao)
                  .WithMany(m => m.Satelites)
                  .HasForeignKey(e => e.MissaoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.ToTable("SENSORES");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Unidade).HasMaxLength(20);
            entity.Property(e => e.Tipo).HasConversion<string>();
            entity.HasOne(e => e.Satelite)
                  .WithMany(s => s.Sensores)
                  .HasForeignKey(e => e.SateliteId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LeituraSensor>(entity =>
        {
            entity.ToTable("LEITURAS_SENSOR");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasOne(e => e.Sensor)
                  .WithMany(s => s.Leituras)
                  .HasForeignKey(e => e.SensorId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}