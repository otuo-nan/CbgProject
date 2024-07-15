
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
    }

    public class TripRiderDto
    {
        public Guid RiderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
    }


    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //public string Region { get; set; }
        public string Name { get; set; }
    }
}
