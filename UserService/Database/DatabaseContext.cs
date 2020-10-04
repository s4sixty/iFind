using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Database.Entities;

namespace UserService.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=ec2-54-75-150-32.eu-west-1.compute.amazonaws.com; " +
                "Database=d4v9prhbdm031o;" +
                "Username=uppnjrwpvcfbes;" +
                "Password=407264492c7e0f90bb49eaec43f69572669630f6a40261f30cf3058f4430d7f0;" +
                "Port=5432;"+
                "Pooling=true;SSL Mode=Require;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //config primary key(Role, User, UserRole)
            modelBuilder.Entity<User>().HasKey(s => s.Id);
            modelBuilder.Entity<Role>().HasKey(s => s.Id);
            modelBuilder.Entity<UserRole>().HasKey(s =>
               new {
                   s.IdRole,
                   s.IdUser
               });

            // Relationships table User,Role,UserRole
            modelBuilder.Entity<UserRole>()
              .HasOne<User>(sc => sc.User)
              .WithMany(s => s.UserRoles)
              .HasForeignKey(sc => sc.IdUser);

            modelBuilder.Entity<UserRole>()
                .HasOne<Role>(sc => sc.Role)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(sc => sc.IdRole);
        }
    }
}
