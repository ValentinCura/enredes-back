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
                entity.Property(e => e.Features).HasColumnType("text[]");
                entity.Property(e => e.Colors).HasColumnType("text[]");
                entity.ToTable("Plans");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

                // Relación FK con Locality
                entity.HasOne(e => e.Locality)
                      .WithMany(l => l.Plans)
                      .HasForeignKey(e => e.LocalityId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.ClientNumber).IsUnique();
                entity.Property(e => e.ClientNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Type).HasDefaultValue("Client");
            });
        }
    }
}
