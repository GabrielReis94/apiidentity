using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MicroserviceIdentityAPI.Domain.Entities.Identity;
using MicroserviceIdentityAPI.Domain.Entities;

namespace MicroserviceIdentityAPI.Infra.Data
{
    public class IdentityContext : IdentityDbContext<User, Role, int, 
                                        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public IdentityContext() { }

        public IdentityContext(DbContextOptions<IdentityContext> options) 
                : base (options){ }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging();
                //.UseSqlServer("Server=PAX00008\\SQLEXPRESS;DataBase=PICKING_ACCESS;user id=sa;password=135451;Trusted_Connection=false;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole => 
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();
                
                userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
            });

            builder.Entity<Cliente>(c =>
            {
                c.HasKey(c => c.Id).HasName("PK_Clientes");
                c.Property(c => c.Id).HasColumnName("IdCliente");                
            });

            builder.Entity<User>(u =>
            {
                u.HasOne(u => u.Clientes)
                 .WithMany(c => c.Users)
                 .HasForeignKey(u => u.ClienteId)
                 .HasConstraintName("FK_Users_Clientes");
            });
        }
    }
}