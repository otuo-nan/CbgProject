using CbgTaxi24.API.Models;

#nullable disable
namespace CbgTaxi24.API.Application.Queries.Dtos
{
    public class TripDto
    {
        public Guid TripId { get; set; }

        public double FromLat { get; set; }
        public double FromLong { get; set; }
        public double ToLat { get; set; }
        public double ToLong { get; set; }
        public string Metadata { get; set; } = default!;

        public decimal Price { get; set; }
        public TripStatus Status { get; set; }

        public Guid RiderId { get; set; }
        public Guid DriverId { get; set; }
    }
    
    public class TripDto2 : TripDto
    {
        public DriverDto Driver { get; set; }
        public TripRiderDto Rider { get; set; }

        public static TripDto2 MapTrip(Trip t)
        {
            return new TripDto2
            {
                TripId = t.TripId,
                FromLat = (double)t.FromLat,
                FromLong = (double)t.FromLong,
                ToLat = (double)t.ToLat,
                ToLong = (double)t.ToLong,
                Price = t.Price,
                Metadata = t.Metadata,
                Status = t.Status,
                RiderId = t.RiderId,
                DriverId = t.DriverId,
                Driver = new DriverDto
                {
                    DriverId = t.Driver.DriverId,
                    Name = t.Driver.Name,
                    Phone = t.Driver.Phone,
                    CarNumber = t.Driver.CarNumber,
                    ServiceType = t.Driver.ServiceType,
                    Status = t.Driver.Status,
                    Rating = t.Driver.Rating,
                },
                Rider = new TripRiderDto
                {
                    RiderId = t.Rider.RiderId,
                    FirstName = t.Rider.FirstName,
                    LastName = t.Rider.LastName,
                    OtherNames = t.Rider.OtherNames,
                }
            };
        }
    }
}
