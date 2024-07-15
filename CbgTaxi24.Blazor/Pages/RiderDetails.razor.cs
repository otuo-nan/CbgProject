using CbgTaxi24.Blazor.Dtos;
using CbgTaxi24.Blazor.Services;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

#nullable disable
namespace CbgTaxi24.Blazor.Pages
{
    public partial class RiderDetails
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        public RiderService Service { get; set; }

        IEnumerable<DriversFromALocationDto> closestDrivers;

        bool isProcessing;
        bool isRequestingTrip;
        string errorMessage = string.Empty;
        RiderDto rider;
        TripDto2 riderTrip;
        FormModel model = new FormModel();

        protected override async Task OnInitializedAsync()
        {
            isProcessing = true;

            await GetRiderAsync();

            if (rider == null)
            {
                errorMessage = "an error occured";
                return;
            }

            closestDrivers = await Service.GetClosestDrivers(rider.Latitude, rider.Longitude, 3);

            isProcessing = false;
        }


        async Task GetRiderAsync()
        {
            rider = await Service.GetRiderAsync(new Guid(Id));

            if (rider.IsInTrip)
            {
                riderTrip = await Service.GetActiveTripAsync(rider.RiderId);
            }
        }

        private string GetRequestingTripButtonStatus() => isRequestingTrip ? "disabled" : string.Empty;

        async Task HandleValidSubmit()
        {
            isRequestingTrip = true;
            await Task.Delay(1000);

            var request = new
            {
                RiderId = Id,
                CurrentLatitude = rider.Latitude,
                CurrentLongitude = rider.Longitude,
                CurrentLocName = rider?.LocationName,
                DestinationLatitude = model.Latitude,
                DestinationLongitude = model.Longitude,
                model.DestinationName
            };

            riderTrip = await Service.RequestRideAsync(request);

            await GetRiderAsync();
            isRequestingTrip = false;
        }
        class FormModel
        {
            [Required]
            public double? Latitude { get; set; } = 5.693774885842252;

            [Required]
            public double? Longitude { get; set; } = -0.24502141829120092;

            [Required]
            public string DestinationName { get; set; } = "Daylight int school, Kwabenya";
        }
    }
}
