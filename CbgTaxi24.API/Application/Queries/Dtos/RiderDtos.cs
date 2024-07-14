
#nullable disable
namespace CbgTaxi24.API.Application.Queries.Dtos
{
    public class RiderDto
    {
        public Guid RiderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string LocationName { get; set; }
    }


    public class LocationDto
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        //public string Region { get; set; }
        public string Name { get; set; }
    }
}
