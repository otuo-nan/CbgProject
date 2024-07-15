using CbgTaxi24.Blazor.SeedWork;
using System.Text.Json.Serialization;

#nullable disable
namespace CbgTaxi24.Blazor.Dtos
{
    public class DriverDto
    {
        public Guid DriverId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CarNumber { get; set; }
        public ServiceType ServiceType { get; set; }
        public DriverStatus Status { get; set; }
        public byte Rating { get; set; }
        public LocationDto Location { get; set; }
    }

    public class DriverDto2
    {
        public Guid DriverId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CarNumber { get; set; }
        public ServiceType ServiceType { get; set; }
        public DriverStatus Status { get; set; }
        public byte Rating { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //public string Region { get; set; }
        public string LocationName { get; set; }
    }

    public class DriversFromALocationDto : DriverDto2
    {
        public double Distance { get; set; }
    }
}
