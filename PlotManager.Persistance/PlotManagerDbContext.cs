using Microsoft.EntityFrameworkCore;
using PlotManager.Domain.Entities;
using PlotManager.Domain.Identity;

namespace PlotManager.Infrastructure.Persistance
{
    public class PlotManagerDbContext : DbContext
    {
        public PlotManagerDbContext(DbContextOptions<PlotManagerDbContext> options) : base(options)
        {
        }

        public DbSet<Plot> Plots { get; set; }
        public DbSet<PlotComplex> PlotComplexes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<PlotFeatures> PlotFeatures { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlotFeatures>()
                .HasKey(x => new { x.PlotId, x.FeatureId });

            modelBuilder.Entity<PlotFeatures>()
                .HasOne(p => p.Plot)
                .WithMany(pf => pf.PlotFeatures)
                .HasForeignKey(p => p.PlotId);

            modelBuilder.Entity<PlotFeatures>()
                .HasOne(f => f.Feature)
                .WithMany(pf => pf.PlotFeatures)
                .HasForeignKey(d => d.FeatureId);

            modelBuilder.Entity<PlotComplex>()
                .HasMany(pc => pc.Plots)
                .WithOne(p => p.PlotComplex)
                .OnDelete(DeleteBehavior.Cascade);

            ConfigureUsers(modelBuilder);
        }

        private void ConfigureUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).HasMaxLength(60).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(60).IsUnicode(false);
                entity.Property(e => e.PasswordHash).HasMaxLength(60).IsUnicode(false);
                entity.HasIndex(e => e.UserName).IsUnique(true);
                entity.HasIndex(e => e.Email).IsUnique(true);
            });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("ca247b62-43b9-42e9-b6b2-02ddc2c4e1f2"),
                    Email = "test",
                    UserName = "test",
                    PasswordHash = "test"
                });

        }
    }
}