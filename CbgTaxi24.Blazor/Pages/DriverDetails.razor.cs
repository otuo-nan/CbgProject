using CbgTaxi24.Blazor.Dtos;
using CbgTaxi24.Blazor.Services;
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
        string errorMessage = string.Empty;
        DriverDto2 entity;

        protected override async Task OnInitializedAsync()
        {
            isProcessing = true;
            entity = await Service.GetDriverAsync(new Guid(Id));

            if (entity == null)
            {
                errorMessage = "an error occured";
            }

            isProcessing = false;
        }
    }
}
