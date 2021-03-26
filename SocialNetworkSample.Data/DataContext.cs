using Microsoft.EntityFrameworkCore;
using SocialNetworkSample.Data.Entities;
using System;

namespace SocialNetworkSample.Data
{
    public class DataContext : DbContext
    {
        private readonly string _connectionString;

        public DataContext(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<SubscriptionEntity> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
            .UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubscriptionEntity>()
                .HasKey(bc => new {bc.PublisherId, bc.SubscriberId});

            modelBuilder.Entity<SubscriptionEntity>()
                .HasOne(bc => bc.Subscriber)
                .WithMany(b => b.Publishers)
                .HasForeignKey(bc => bc.SubscriberId);

            modelBuilder.Entity<SubscriptionEntity>()
                .HasOne(bc => bc.Publisher)
                .WithMany(b => b.Subscribers)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(bc => bc.PublisherId);
        }
    }
}