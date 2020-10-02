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
    }
}
