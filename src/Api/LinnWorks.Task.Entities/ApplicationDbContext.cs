using System;
using Amazon.SecretsManager;
using LinnWorks.AWS.SecretsManager;
using Microsoft.EntityFrameworkCore;

namespace LinnWorks.Task.Entities
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionString = "Server=linnworks.c4tbstnbpx2e.eu-central-1.rds.amazonaws.com;Database=LinnWorks; User Id=sa;Password=Bd1601211";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<ItemType> ItemTypes { get; set; }

        public DbSet<OrderPriority> OrderPriorities { get; set; }

        public DbSet<SalesChannel> SalesChannels { get; set; }
    }
}