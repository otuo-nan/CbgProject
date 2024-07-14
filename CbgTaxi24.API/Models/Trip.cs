using System.Text.Json.Serialization;

namespace CbgTaxi24.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TripStatus : byte
    {
        Active = 1, Completed = 2
    }

    public class Trip : Entity
    {
        public Guid TripId { get; set; }

        public decimal FromLat { get; set; }
        public decimal FromLong { get; set; }
        public decimal ToLat { get; set; }
        public decimal ToLong { get; set; }

        public decimal Price { get; set; }

        //data storage for invoice purposes
        public string Metadata { get; set; } = default!;
        public TripStatus Status { get; set; }

        //rel
        public Guid RiderId { get; set; }
        public Guid DriverId { get; set; }

        public Driver Driver { get; set; } = default!;
        public Rider Rider { get; set; } = default!;
    }

    public class TripMetaData
    {
        public LocationDto FromLocation { get; set; } = default!;
        public LocationDto ToLocation { get; set; } = default!;
    }
}
