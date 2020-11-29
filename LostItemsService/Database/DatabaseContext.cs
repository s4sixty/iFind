using LostItemsService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostItemsService.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Comments> Comments { get; set; }
        public DbSet<LostItem> LostItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=ec2-52-30-161-203.eu-west-1.compute.amazonaws.com; " +
                "Database=ddq3k9117eogiu;" +
                "Username=fdphnwuapyxtws;" +
                "Password=3b2b1331454c108faddff6eb627ad4785f0630a3e5ab22c586e01df00f608f6b;" +
                "Port=5432;" +
                "Pooling=true;SSL Mode=Require;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LostItem>()
                .HasMany(c => c.Comments)
                .WithOne(e => e.LostItem);
        }
    }
}
