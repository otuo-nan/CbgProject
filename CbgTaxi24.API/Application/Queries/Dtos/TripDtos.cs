using CbgTaxi24.API.Models;

namespace CbgTaxi24.API.Application.Queries.Dtos
{
    public class TripDto
    {
        public Guid TripId { get; set; }

        public double FromLat { get; set; }
        public double FromLong { get; set; }
        public double ToLat { get; set; }
        public double ToLong { get; set; }

        public decimal Price { get; set; }
        public TripStatus Status { get; set; }

        public Guid RiderId { get; set; }
        public Guid DriverId { get; set; }
    }
}
