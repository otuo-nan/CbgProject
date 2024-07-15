using CbgTaxi24.Blazor.Components.Drivers;
using CbgTaxi24.Blazor.Dtos;
using CbgTaxi24.Blazor.SeedWork;
using CbgTaxi24.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CbgTaxi24.Blazor.Pages
{
    public partial class Riders
    {
        #region fields
        IEnumerable<RiderDto> listEntities = default!;
        bool _isProcessing;

        [Inject]
        public IJSRuntime JS { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public ILogger<Riders> Logger { get; set; } = null!;


        [Inject]
        public RiderService Service { get; set; }

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

            PagedData<RiderDto>? paged = await Service.GetRidersAsync(pageMetaData.CurrentPage, pageMetaData.PageSize);

            if (paged != null)
            {
                listEntities = paged.Data;

                pageMetaData.TotalPages = paged.PagingOptions.TotalPages;
                pageMetaData.PageSize = paged.PagingOptions.PageSize;
                pageMetaData.TotalCount = paged.PagingOptions.TotalCount;
            }

            _isProcessing = false;
        }

        void ListRow_DbClicked(RiderDto dto)
        {
            NavigationManager.NavigateTo($"rider-details/{dto.RiderId}");
        }

        #region Paging
        async Task Sort(string sortField)
        {
            if (sortField.Equals(pagingOptions.CurrentSortField))
            {
                pagingOptions.CurrentSortDirection =
                    pagingOptions.CurrentSortDirection.Equals("Asc") ? "Desc" : "Asc";
            }
            else
            {
                pagingOptions.CurrentSortField = sortField;
                pagingOptions.CurrentSortDirection = "Asc";
            }

            await GetEntitiesAsync();
        }

        string SortIndicator(string sortField)
        {
            if (sortField.Equals(pagingOptions.CurrentSortField))
            {
                return pagingOptions.CurrentSortDirection.Equals("Asc") ? "fas fa-sort-down" : "fas fa-sort-up";
            }

            return "fas fa-sort-down";
        }

        protected async Task SelectedPage_Callback()
        {
            await GetEntitiesAsync();
        }
        #endregion
    }

}
