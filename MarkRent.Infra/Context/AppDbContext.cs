using MarkRent.Domain.Entities;
using MarkRent.Infra.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Infra.Context
{
    public class AppDbContext : DbContext
    {

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<FutureEvent> FutureEvents { get; set; }
        public DbSet<DeliveryAgent> DeliveryAgents { get; set; }
        public DbSet<PriceDay> PriceDays { get; set; }
        public DbSet<Hire> Hires { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { Database.EnsureCreated(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VehicleMapping());
            modelBuilder.ApplyConfiguration(new DeliveryAgentMapping());
            modelBuilder.ApplyConfiguration(new FutureEventMapping());
            modelBuilder.ApplyConfiguration(new PriceDayMapping());
            modelBuilder.ApplyConfiguration(new HireMapping());
        }
    }
}
