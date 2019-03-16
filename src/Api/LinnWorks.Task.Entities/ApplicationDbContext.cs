using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LinnWorks.Task.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=linnworks.c4tbstnbpx2e.eu-central-1.rds.amazonaws.com;Database=LinnWorks;User Id=sa; Password=Bd1601211");
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