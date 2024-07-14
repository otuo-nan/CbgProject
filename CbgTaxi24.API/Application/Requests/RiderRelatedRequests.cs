namespace CbgTaxi24.API.Application.Requests
{
	public class RideRequest
	{
        public double CurrentLatitude { get; set; }
        public double CurrentLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
    }
}
