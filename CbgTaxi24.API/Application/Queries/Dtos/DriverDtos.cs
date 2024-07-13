using CbgTaxi24.API.Application.Requests;
using CbgTaxi24.API.Models;
using System.Text.Json.Serialization;

#nullable disable
namespace CbgTaxi24.API.Application.Queries.Dtos
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilterDriversBy : byte
    {
        ServiceType = 1, DriverStatus = 2,
    }

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
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        //public string Region { get; set; }
        public string LocationName { get; set; }
    }

    public class DriversFromALocationDto : DriverDto2
    {
        public decimal Distance { get; set; }
    }

    public class DriversPagingOptions : PagingOptions
    {
        public DriversPagingOptions(PageRequest request)
        {
            if (request is DriversPageRequest driverRequest)
            {
                FilterBy = driverRequest.FilterBy;
                FilterByValue = driverRequest.FilterByValue;
                MapRequestToThis(request);
            }
            else
            {
                throw new ArgumentException($"constructor argument must be of type {nameof(DriversPageRequest)}");
            }
        }

        public FilterDriversBy? FilterBy { get; set; }

        public string FilterByValue { get; set; }
    }
    
    public class DriversWithinSpecificLocationPagingOptions : PagingOptions
    {
        public DriversWithinSpecificLocationPagingOptions(PageRequest request)
        {
            if (request is DriversWithinSpecificLocationPageRequest driverRequest)
            {
                Latitude = driverRequest.Latitude;
                Longitude = driverRequest.Longitude;
                MaxRangeFromLocationInKm = driverRequest.MaxRangeFromLocationInKm;
                FilterBy = driverRequest.FilterBy;
                FilterByValue = driverRequest.FilterByValue;
                MapRequestToThis(request);
            }
            else
            {
                throw new ArgumentException($"constructor argument must be of type {nameof(DriversWithinSpecificLocationPageRequest)}");
            }
        }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public float MaxRangeFromLocationInKm { get; set; }
        public FilterDriversBy? FilterBy { get; set; }

        public string FilterByValue { get; set; }
    }
}
