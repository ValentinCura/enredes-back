using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Plan> Plans => Set<Plan>();
        public DbSet<Locality> Localities => Set<Locality>();
        public DbSet<PlanLocality> PlanLocalities => Set<PlanLocality>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Locality>(entity =>
            {
                entity.ToTable("Localities");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Cod).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Province).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Status).HasDefaultValue(true);
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plans");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Features).HasColumnType("text[]");
                entity.Property(e => e.Status).HasDefaultValue(true);
            });

            // Tabla intermedia N a M
            modelBuilder.Entity<PlanLocality>(entity =>
            {
                entity.ToTable("PlanLocalities");
                entity.HasKey(e => new { e.PlanId, e.LocalityId });

                entity.HasOne(e => e.Plan)
                      .WithMany(p => p.PlanLocalities)
                      .HasForeignKey(e => e.PlanId);

                entity.HasOne(e => e.Locality)
                      .WithMany(l => l.PlanLocalities)
                      .HasForeignKey(e => e.LocalityId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Type).HasDefaultValue("Client");
                entity.Property(e => e.Status).HasDefaultValue(true);
            });
        }
    }
}