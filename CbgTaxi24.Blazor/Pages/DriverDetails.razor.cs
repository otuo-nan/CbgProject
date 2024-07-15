using CbgTaxi24.Blazor.Dtos;
using Microsoft.AspNetCore.Components;

#nullable disable
namespace CbgTaxi24.Blazor.Pages
{
    public partial class DriverDetails
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        public DriverService Service { get; set; }

        bool isProcessing;
        bool isCompletingTrip;
        string errorMessage = string.Empty;
        DriverDto2 driver;
        TripDto2 driverTrip;

        protected override async Task OnInitializedAsync()
        {
            isProcessing = true;
            await GetDriverAsync();

            if (driver == null)
            {
                errorMessage = "an error occured";
            }

            if (driver.Status == DriverStatus.InTrip)
            {
                driverTrip = await Service.GetActiveTripAsync(new Guid(Id));
            }

            isProcessing = false;
        }

        async Task GetDriverAsync()
        {
            driver = await Service.GetDriverAsync(new Guid(Id));
        }

        async Task CompleteTrip_Clicked()
        {
            isCompletingTrip = true;
            await Service.CompleteTripAsync(driverTrip.TripId);

            await GetDriverAsync();

            isCompletingTrip = false;  
        }

        private string GetCompleteTripButtonStatus() => isCompletingTrip ? "disabled" : string.Empty;

    }
}
