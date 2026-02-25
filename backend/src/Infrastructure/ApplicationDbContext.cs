using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Aquí registramos nuestra entidad User
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
