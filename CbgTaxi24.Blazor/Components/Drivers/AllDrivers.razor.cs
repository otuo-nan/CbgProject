using CbgTaxi24.Blazor.Dtos;
using CbgTaxi24.Blazor.SeedWork;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CbgTaxi24.Blazor.Components.Drivers
{
    public partial class AllDrivers
    {
        IEnumerable<DriverDto> listEntities = default!;
        string searchWidth = "col-md-7", filterWidth = "col-md-2", filterValueWidth = "col-md-3";
        bool _isProcessing;

        [Inject]
        public IJSRuntime JS { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public ILogger<AllDrivers> Logger { get; set; } = null!;


        [Inject]
        public DriverService Service { get; set; }

        PagingOptions pagingOptions = new()
        {
            CurrentSortField = "CreatedOn",
            CurrentSortDirection = "Desc"
        };

        protected PageMetaData pageMetaData = new();

        DriverPagerOptions driverPagerOptions = new();


        private string _searchQuery = string.Empty;
        private CancellationTokenSource currentSearchCts = null!;

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

            PagedData<DriverDto>? paged = await Service.GetAllDriversAsync(driverPagerOptions, pageMetaData);

            if (paged != null)
            {
                listEntities = paged.Data;

                pageMetaData.TotalPages = paged.PagingOptions.TotalPages;
                pageMetaData.PageSize = paged.PagingOptions.PageSize;
                pageMetaData.TotalCount = paged.PagingOptions.TotalCount;
            }

            _isProcessing = false;
        }

        void ListRow_DbClicked(DriverDto dto)
        {
            NavigationManager.NavigateTo($"driver-details/{dto.DriverId}");
        }

        #region filtering
        List<string> filterValuesDropdown = new();

        public DriverListFilterBy SelectedFilterOption { set; get; }

        public async Task FilterSelected(ChangeEventArgs e)
        {
            SelectedFilterOption = Enum.Parse<DriverListFilterBy>((string)e.Value!);

            switch (SelectedFilterOption)
            {
                case DriverListFilterBy.None:
                    {
                        filterValuesDropdown = new();
                        driverPagerOptions.FilterBy = DriverListFilterBy.None;
                        searchWidth = "col-md-7";
                        filterWidth = "col-md-2";
                        filterValueWidth = "col-md-3";
                        await GetEntitiesAsync();
                        break;
                    }
                case DriverListFilterBy.ServiceType:
                    {
                        filterValuesDropdown = new List<string>
                        {
                            FilterOption.Select,
                            ServiceType.Standard.ToString(),
                            ServiceType.Premium.ToString(),
                            ServiceType.Delivery.ToString(),
                        };
                        driverPagerOptions.FilterBy = DriverListFilterBy.ServiceType;
                        searchWidth = "col col-md-7";
                        filterWidth = "col-md-2";
                        filterValueWidth = "col-md-3";
                        break;
                    }
                case DriverListFilterBy.DriverStatus:
                    {
                        filterValuesDropdown = new List<string>
                        {
                            FilterOption.Select,
                            DriverStatus.Available.ToString(),
                            DriverStatus.Unavailable.ToString(),
                            DriverStatus.Suspended.ToString(),
                        };
                        driverPagerOptions.FilterBy = DriverListFilterBy.DriverStatus;
                        searchWidth = "col col-md-7";
                        filterWidth = "col-md-2";
                        filterValueWidth = "col-md-3";
                        break;
                    }
                default:
                    break;
            }
        }

        public async Task StatusSelected(ChangeEventArgs e)
        {
            var selected = (string)e.Value!;

            if (selected == FilterOption.Select)
            {
                driverPagerOptions.FilterByValue = string.Empty;
            }
            else
            {
                driverPagerOptions.FilterByValue = selected;
            }

            await GetEntitiesAsync();
        }
        #endregion

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

    static class FilterOption
    {
        public const string Select = "Select ...";
        public const string NoFilter = "NoFilter";
        public const string Status = "Status";
        public const string Date = "Date";
    }

    public enum DriverListFilterBy : Byte
    {
        None = 0, ServiceType = 1, DriverStatus = 2,

    }

    public class DriverPagerOptions
    {
        public string OrderBy { get; set; } = default!;
        public string OrderDirection { get; set; } = "Asc";
        public DriverListFilterBy FilterBy { get; set; } = DriverListFilterBy.None;

        //value gained from second dropdown
        public string FilterByValue { get; set; } = string.Empty;

        //date filters
        public DateTime CreatedOnStart { get; set; }
        public DateTime? CreatedOnEnd { get; set; }
    }
}
