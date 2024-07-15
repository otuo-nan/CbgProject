namespace CbgTaxi24.Blazor.Services
{
    public class BackOfficeService
    {
        readonly HttpClient _httpClient;
        public BackOfficeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
