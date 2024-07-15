using CbgTaxi24.Blazor.Components.Drivers;
using CbgTaxi24.Blazor.Dtos;
using CbgTaxi24.Blazor.SeedWork;

namespace CbgTaxi24.Blazor.Services
{
    public class DriverService
    {
        readonly HttpClient _httpClient;
        ILogger<DriverService> _logger;
        public DriverService(HttpClient httpClient, ILogger<DriverService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<DriverDto2?> GetDriverAsync(Guid id)
        {
            try
            {
                return (await _httpClient.GetFromJsonAsync<DriverDto2>($"drivers/{id}"))!;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return default;
            }
        }

        public async Task<PagedData<DriverDto>?> GetAllDriversAsync(DriverPagerOptions pagerOptions, PageMetaData pageMetaData)
        {
            var httpParams = $"drivers?PageNum={pageMetaData.CurrentPage}&PageSize={pageMetaData.PageSize}";

            if (pagerOptions.FilterBy != DriverListFilterBy.None)
            {
                httpParams += $"&filterBy={pagerOptions.FilterBy.ToString()}&filterByValue={pagerOptions.FilterByValue}";
            }

            try
            {
                return (await _httpClient.GetFromJsonAsync<PagedData<DriverDto>>(httpParams))!;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return default;
            }
        }

        public async Task<PagedData<DriversFromALocationDto>?> GetDriversWithinSpecificLocationAsync(double locLatitude, double locLongitude,
            double maxRangeFromLocation)
        {
            var httpParams = $"drivers/from-location?Latitude={locLatitude}&Longitude={locLongitude}&MaxRangeFromLocationInKm={maxRangeFromLocation}&PageNum=1&PageSize=1000";
            try
            {
                return (await _httpClient.GetFromJsonAsync<PagedData<DriversFromALocationDto>>(httpParams))!;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return default;
            }
        }

        public static string GetDriverStatusCss(DriverStatus status)
        {
            return status switch
            {
                DriverStatus.Suspended => "text-danger",
                DriverStatus.Available => "text-success",
                DriverStatus.Unavailable => "text-info",
                _ => string.Empty,
            };
        }

        public static string GetServiceTypeCss(ServiceType type)
        {
            return type switch
            {
                ServiceType.Standard => "text-primary",
                ServiceType.Premium => "text-warning",
                ServiceType.Delivery => "text-info",
                _ => string.Empty,
            };
        }
    }
}
