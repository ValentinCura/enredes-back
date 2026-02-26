using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Aquí registramos nuestra entidad User
        public DbSet<User> Users => Set<User>();
        public DbSet<Plan> Plans => Set<Plan>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plans"); // O el nombre que prefieras para la tabla
                entity.HasKey(e => e.Id);

                // Esto asegura que Postgres maneje el autoincremento correctamente
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                // Configuración para el precio (opcional pero recomendado para evitar advertencias de precisión)
                entity.Property(e => e.Price)
                      .HasColumnType("decimal(18,2)");
            });

            // Configuramos las reglas de la tabla Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);

                // Índices Únicos (como en tu script de SQL Server)
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.ClientNumber).IsUnique();

                // Restricciones de longitud y obligatoriedad
                entity.Property(e => e.ClientNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Type).HasDefaultValue("Client");
            });
        }
    }
}
