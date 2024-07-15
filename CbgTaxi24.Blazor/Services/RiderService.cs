using CbgTaxi24.Blazor.Dtos;
using CbgTaxi24.Blazor.SeedWork;
using System.Text.Json;

namespace CbgTaxi24.Blazor.Services
{
    public class RiderService
    {
        readonly HttpClient _httpClient;
        readonly ILogger<RiderService> _logger;
        public RiderService(HttpClient httpClient, ILogger<RiderService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<PagedData<RiderDto>?> GetRidersAsync(int pageNum = 1, int pageSize = 10)
        {
            try
            {
                var httpParams = $"riders?PageNum={pageNum}&PageSize={pageSize}";
                return (await _httpClient.GetFromJsonAsync<PagedData<RiderDto>>(httpParams))!;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return default;
            }
        }

        public async Task<RiderDto?> GetRiderAsync(Guid id)
        {
            try
            {
                return (await _httpClient.GetFromJsonAsync<RiderDto>($"riders/{id}"))!;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return default;
            }
        }

        public async Task<IEnumerable<DriversFromALocationDto>?> GetClosestDrivers(double riderLocLatitude, double riderLocLongitude, int nClosestDrivers)
        {
            try
            {
                var @params = $"riders/closest-drivers?riderLocLatitude={riderLocLatitude}&riderLocLongitude={riderLocLongitude}&nClosestDrivers={nClosestDrivers}";
                return (await _httpClient.GetFromJsonAsync<IEnumerable<DriversFromALocationDto>>(@params))!;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return default;
            }
        }

        public async Task<TripDto2> RequestRideAsync(object requestBody)
        {
            var response = await _httpClient.PostAsJsonAsync("riders/request-ride", requestBody);

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<TripDto2>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<TripDto2> GetActiveTripAsync(Guid id)
        {
           return (await _httpClient.GetFromJsonAsync<TripDto2>($"riders/active-trip/{id}"))!;
        }
    }
}
