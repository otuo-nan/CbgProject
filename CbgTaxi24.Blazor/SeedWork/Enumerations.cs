using System.Text.Json.Serialization;

namespace CbgTaxi24.Blazor.SeedWork
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ServiceType : byte
    {
        Standard = 1, Premium = 2, Delivery = 3
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DriverStatus : byte
    {
        Available = 1, Unavailable = 2, Suspended = 3
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilterDriversBy : byte
    {
        ServiceType = 1, DriverStatus = 2,
    }

}
