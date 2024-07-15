using CbgTaxi24.Blazor.Dtos;
using CbgTaxi24.Blazor.SeedWork;

namespace CbgTaxi24.Blazor.Services
{
    public class BackOfficeService
    {
        readonly HttpClient _httpClient;
        readonly ILogger<BackOfficeService> _logger;
        public BackOfficeService(HttpClient httpClient, ILogger<BackOfficeService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<PagedData<TripDto2>> GetAllTripsAsync(int pageNum = 1, int pageSize = 10)
        {
            var httpParams = $"backoffice/trips?filter=All&PageNum={pageNum}&PageSize={pageSize}";
            return (await _httpClient.GetFromJsonAsync<PagedData<TripDto2>>(httpParams))!;
        }
    }
}
