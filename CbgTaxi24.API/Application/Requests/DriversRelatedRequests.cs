#nullable disable
namespace CbgTaxi24.API.Application.Requests
{
    public class DriversPageRequest : PageRequest
    {
        public FilterDriversBy? FilterBy { get; set; }
        public string FilterByValue { get; set; }
    }

    public class DriversWithinSpecificLocationPageRequest : PageRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public float MaxRangeFromLocationInKm { get; set; }
        public FilterDriversBy? FilterBy { get; set; }
        public string FilterByValue { get; set; }
    }
}
