using CbgTaxi24.Blazor.Dtos;
using CbgTaxi24.Blazor.SeedWork;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CbgTaxi24.Blazor.Pages
{
    public partial class Trips
    {
        #region fields
        IEnumerable<TripDto2> listEntities = default!;
        bool _isProcessing;

        [Inject]
        public IJSRuntime JS { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public ILogger<Riders> Logger { get; set; } = null!;


        [Inject]
        public BackOfficeService Service { get; set; }

        PagingOptions pagingOptions = new()
        {
            CurrentSortField = "CreatedOn",
            CurrentSortDirection = "Desc"
        };

        protected PageMetaData pageMetaData = new();

        private string _searchQuery = string.Empty;
        private CancellationTokenSource currentSearchCts = null!;

        #endregion fields
        string SearchQuery
        {
            get => _searchQuery;

            set
            {
                _searchQuery = value;
                _ = SearchDebouncedAsync();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await GetEntitiesAsync();
        }

        async Task SearchDebouncedAsync()
        {
            try
            {
                currentSearchCts?.Cancel();
                currentSearchCts = new CancellationTokenSource();
                var thisSearchToken = currentSearchCts.Token;

                await Task.Delay(500);

                if (!thisSearchToken.IsCancellationRequested)
                {
                    pageMetaData.Reset();
                    await GetEntitiesAsync(thisSearchToken);
                }

                StateHasChanged();
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex.StackTrace);
            }
        }

        async Task GetEntitiesAsync(CancellationToken cancellationToken = default)
        {
            _isProcessing = true;

            PagedData<TripDto2>? paged = await Service.GetAllTripsAsync(pageMetaData.CurrentPage, pageMetaData.PageSize);

            if (paged != null)
            {
                listEntities = paged.Data;

                pageMetaData.TotalPages = paged.PagingOptions.TotalPages;
                pageMetaData.PageSize = paged.PagingOptions.PageSize;
                pageMetaData.TotalCount = paged.PagingOptions.TotalCount;
            }

            _isProcessing = false;
        }

        static string GetTripStatusCss(TripDto2 trip)
        {
            return trip.Status switch
            {
                TripStatus.Active => "text-success",
                TripStatus.Completed => "text-primary",
                _ => throw new ArgumentException("invalid trip status"),
            };
        }

        static string GetFromLocation(TripMetaData meta)
        {
            return string.IsNullOrEmpty(meta.FromLocation.Name) ? $"Lat: {meta.FromLocation.Latitude}, Long: {meta.FromLocation.Longitude}" : meta.FromLocation.Name;
        }
        
        static string GetDestiantion(TripMetaData meta)
        {
            return string.IsNullOrEmpty(meta.ToLocation.Name) ? $"Lat: {meta.ToLocation.Latitude}, Long: {meta.ToLocation.Longitude}" : meta.ToLocation.Name;
        }

        #region Paging
        protected async Task SelectedPage_Callback()
        {
            await GetEntitiesAsync();
        }
        #endregion
    }
}
