using System;
using System.Collections.Generic;
using System.Text;
using Amazon.SecretsManager;
using LinnWorks.AWS.SecretsManager;
using Microsoft.EntityFrameworkCore;

namespace LinnWorks.Task.Entities
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IAmazonSecretsManager secretsManager;

        public ApplicationDbContext(IAmazonSecretsManager secretsManager)
        {
            this.secretsManager = secretsManager ?? throw new ArgumentNullException(nameof(secretsManager));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            DBConnectionString connectionStringObject = SecretsManager.GetConnectionString(secretsManager);
            string connectionString = string.Format("Server={0};Database={1};User Id={2}; Password={3}", connectionStringObject.Host, connectionStringObject.DbInstanceIdentifier, connectionStringObject.Username, connectionStringObject.Password);
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