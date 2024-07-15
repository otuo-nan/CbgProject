namespace CbgTaxi24.Blazor.Services
{
    public class RiderService
    {
        readonly HttpClient _httpClient;
        public RiderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
