namespace CbgTaxi24.API.Models
{
    public enum RiderStatus : byte
    {

    }
    public class Rider : Entity
    {
        public Guid RiderId { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OtherNames { get; set; }
        public bool IsInTrip { get; set; }
        //rel
        public Guid LocationId { get; set; }
        public Location Location { get; set; } = default!;
        public ICollection<Trip> Trips { get; set; } = [];
        public ICollection<Invoice> Invoices { get; set; } = [];
    }
}
