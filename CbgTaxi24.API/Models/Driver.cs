using System.Text.Json.Serialization;

namespace CbgTaxi24.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ServiceType: byte
    {
        Standard = 1, Premium = 2, Delivery = 3
    }

    public enum DriverStatus : byte
    {
        Active = 1, InActive = 2, Suspended = 3
    }

    public class Driver
    {
        public Guid DriverId { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string CarNumber { get; set; }

        public ServiceType ServiceType { get; set; }
        public DriverStatus Status { get; set; }
        public byte Rating { get; set; }

        //rel
        public Guid LocationId { get; set; }
        public Location Location { get; set; } = default!;
        public ICollection<Trip> Trips { get; set; } = [];
    }
}
