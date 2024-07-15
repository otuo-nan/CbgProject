
#nullable disable
namespace CbgTaxi24.Blazor.Dtos
{
    public class RiderDto
    {
        public Guid RiderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string LocationName { get; set; }
        public bool IsInTrip { get; set; }
        public string Name => $"{FirstName} {LastName}";
    }

    public class TripRiderDto
    {
        public Guid RiderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string Name => $"{FirstName} {LastName}";
    }


    public class LocationDto
    {
        private double latitude;
        private double longitude;

        public double Latitude { get => double.Round(latitude, 6); set => latitude = value; }
        public double Longitude { get => double.Round(longitude, 6); set => longitude = value; }
        //public string Region { get; set; }
        public string Name { get; set; }
    }
}
