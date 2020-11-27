using FoundItemsService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundItemsService.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<FoundItem> FoundItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=ec2-176-34-114-78.eu-west-1.compute.amazonaws.com; " +
                "Database=d3p57q70re8vnk;" +
                "Username=tthmvfdpbdrhig;" +
                "Password=ed5572d8d214462b16d206cd0f5a009f1a505d65a49435310d4608954a1e4ad8;" +
                "Port=5432;" +
                "Pooling=true;SSL Mode=Require;TrustServerCertificate=True;");
        }
    }
}
