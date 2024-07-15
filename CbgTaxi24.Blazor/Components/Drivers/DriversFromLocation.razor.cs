using CbgTaxi24.Blazor.Dtos;
using CbgTaxi24.Blazor.Services;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace CbgTaxi24.Blazor.Components.Drivers
{
    public partial class DriversFromLocation
    {
        [Inject]
        public DriverService Service { get; set; }

        IEnumerable<DriversFromALocationDto> listEntities;
        bool isProcessing;
        FormModel model = new();  

        async Task HandleValidSubmit()
        {
            isProcessing = true;

            var pagedData = await Service.GetDriversWithinSpecificLocationAsync(model.Latitude.Value, model.Longitude.Value, model.WithinDistance.Value);
            listEntities = pagedData.Data;
            isProcessing = false;   
        }

        class FormModel
        {
            [Required]
            public double? Latitude { get; set; } = 5.662276533292582;

            [Required]
            public double? Longitude { get; set; } = -0.20558562416544685;

            [Required(ErrorMessage = "The distance field is required")]
            public double? WithinDistance { get; set; } = 3;
        }
    }
}
