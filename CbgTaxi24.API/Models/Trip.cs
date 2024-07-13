namespace CbgTaxi24.API.Models
{
    public class Trip
    {
        public Guid TripId { get; set; }

        public decimal FromLat { get; set; }
        public decimal FromLong { get; set; }
        public decimal ToLat { get; set; }
        public decimal ToLong { get; set; }

        public decimal Price { get; set; }

        //rel
        public Driver Driver { get; set; } = default!;
        public Rider Rider { get; set; } = default!;
    }
}
