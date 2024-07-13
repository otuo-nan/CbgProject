namespace CbgTaxi24.API.Models
{
    public class Rider
    {
        public Guid RiderId { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OtherNames { get; set; }

        //rel
        public Guid LocationId { get; set; }
        public Location Location { get; set; } = default!;
        public ICollection<Trip> Trips { get; set; } = [];
        public ICollection<Invoice> Invoices { get; set; } = [];
    }
}
