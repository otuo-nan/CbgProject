using CbgTaxi24.Blazor.SeedWork;
using System.Text.Json;

#nullable disable
namespace CbgTaxi24.Blazor.Dtos
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

        private TripMetaData _tripMetadata = null;

        public TripMetaData TripMetaData
        {
            get
            {
                if (!string.IsNullOrEmpty(Metadata))
                {
                    if (_tripMetadata == null)
                    {
                        _tripMetadata = JsonSerializer.Deserialize<TripMetaData>(Metadata);

                        return _tripMetadata;
                    }
                    return _tripMetadata;
                }

                return default!;
            }
        }
    }

    public class TripDto2 : TripDto
    {
        public DriverDto Driver { get; set; }
        public TripRiderDto Rider { get; set; }
    }

    public class TripMetaData
    {
        public LocationDto FromLocation { get; set; } = default!;
        public LocationDto ToLocation { get; set; } = default!;
    }
}
