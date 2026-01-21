using Microsoft.EntityFrameworkCore;
using APIWEB.src.Features.User.Domain.Entity;

namespace APIWEB.src.Shared.Infrastructure.Configurations
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");

            // Configuração da tabela User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasKey(e => e.InternalId);

                entity.Property(e => e.InternalId)
                    .UseIdentityColumn()
                    .HasColumnName("internal_id");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(36)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at");

                entity.HasIndex(e => e.Email)
                    .IsUnique();
            });
        }
    }
}