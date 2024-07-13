
#nullable disable
namespace CbgTaxi24.API.Application.Queries.Dtos
{
    public class RiderDto
    {
    }


    public class LocationDto
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        //public string Region { get; set; }
        public string Name { get; set; }
    }
}
