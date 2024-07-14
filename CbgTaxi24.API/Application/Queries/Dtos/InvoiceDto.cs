namespace CbgTaxi24.API.Application.Queries.Dtos
{
    public class InvoiceDto
    {
        public Guid InvoiceId { get; set; }
        public required string RiderName { get; set; }
        public required string DriverName { get; set; }
        public required string LicensePlate { get; set; }
        public double Price { get; set; }
        public double FromLat { get; set; }
        public double FromLong { get; set; }
        public string? FromName { get; set; }

        public double ToLat { get; set; }
        public double ToLong { get; set; }
        public string? ToName { get; set; }
    }
}
