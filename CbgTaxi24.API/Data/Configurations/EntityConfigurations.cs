using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CbgTaxi24.API.Models;

namespace CbgTaxi24.API.Data.Configurations
{
    public class LocationConfigurations : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> entity)
        {
            entity.Property(p => p.Latitude)
                .HasColumnType("decimal(8, 6)");

            entity.Property(p => p.Longitude)
                .HasColumnType("decimal(9, 6)");
        }
    }
    
    public class TripConfigurations : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> entity)
        {
            entity.Property(p => p.FromLat)
                .HasColumnType("decimal(8, 6)");

            entity.Property(p => p.FromLong)
                .HasColumnType("decimal(9, 6)");
            
            entity.Property(p => p.ToLat)
                .HasColumnType("decimal(8, 6)");

            entity.Property(p => p.ToLong)
                .HasColumnType("decimal(9, 6)");
            
            entity.Property(p => p.Price)
                .HasColumnType("decimal(9, 2)");
        }
    }
}
