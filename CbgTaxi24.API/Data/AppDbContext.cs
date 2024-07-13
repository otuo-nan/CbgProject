using CbgTaxi24.API.Data.Configurations;
using CbgTaxi24.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CbgTaxi24.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Rider> Riders { get; set; }    
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new LocationConfigurations());
            builder.ApplyConfiguration(new TripConfigurations());

            base.OnModelCreating(builder);
        }
    }
}
