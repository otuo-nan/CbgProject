namespace CbgTaxi24.API.Models
{
    public enum TripStatus : byte
    {
        Active = 1, Completed = 2
    }

    public class Trip
    {
        public Guid TripId { get; set; }

        public decimal FromLat { get; set; }
        public decimal FromLong { get; set; }
        public decimal ToLat { get; set; }
        public decimal ToLong { get; set; }

        public decimal Price { get; set; }
        public TripStatus Status { get; set; }

        //rel
        public Guid RiderId { get; set; }
        public Guid DriverId { get; set; }

        public Driver Driver { get; set; } = default!;
        public Rider Rider { get; set; } = default!;
    }
}
