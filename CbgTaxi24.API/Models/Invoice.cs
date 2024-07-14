namespace CbgTaxi24.API.Models
{
    public class Invoice : Entity
    {
        public Guid InvoiceId { get; set; }
        public decimal Price { get; set; }

        public Guid RiderId { get; set; }
        public Guid TripId { get; set; }

        public Rider Rider { get; set; } = default!;
        public Trip Trip { get; set; } = default!;
    }
}
