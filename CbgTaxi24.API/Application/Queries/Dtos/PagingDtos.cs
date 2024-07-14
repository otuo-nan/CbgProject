using CbgTaxi24.API.Infrastructure.Exceptions;

namespace CbgTaxi24.API.Application.Queries.Dtos
{
    public class PageRequest
    {
        public string? SortField { get; set; }
        public string SortDirection { get; set; } = "Desc";
        public string? SearchString { get; set; }
        public int PageNum { get; set; } = 1;
        public int PageSize { get; set; } = 15;
    }

    public class PaginatedEntities<TOptions, TData>  where TOptions : PagingOptions
    {
        public required TOptions PagingOptions { get; set; } = default!;
        public required IEnumerable<TData> Data { get; set; } = [];
    }

    public class PagingOptions
    {
        public string? SortField { get; set; }
        public string SortDirection { get; set; } = "Desc";
        public string? SearchString { get; set; }

        public int PageNum { get; set; } = 1;
        public double TotalCount { get; private set; }
        public int PageSize { get; set; } = 15;

        public int[] PageSizes = [15, 30, 50, 100, 500, 1000];

        public int NumPages { get; set; } = 1;


        #region helpers
        public void SetUpRestOfDto(long queryCount)
        {
            TotalCount = queryCount;

            if (TotalCount > 0)
                NumPages = (int)Math.Ceiling(TotalCount / PageSize);

            //used if PageNum posted value is greater than the calculated number of pages
            PageNum = TotalCount == 0 ? 1 : Math.Min(Math.Max(1, PageNum), NumPages);
        }

        //Validator
        public void ValidateOptions<TQueryObject>()
        {
            if (string.IsNullOrEmpty(SortDirection) || (SortDirection.ToUpper() != "DESC" && SortDirection.ToUpper() != "ASC"))
            {
                throw new PlatformException("sortDirection field invalid");
            }

            if (!string.IsNullOrEmpty(SortField) && typeof(TQueryObject).GetProperties().Any(p => p.Name == SortField) is false)
            {
                throw new PlatformException("sortField invalid");
            }
        }

        public void MapRequestToThis(PageRequest request)
        {
            SortField = request.SortField;
            SortDirection = request.SortDirection;
            SearchString = request.SearchString;
            PageNum = request.PageNum;
            PageSize = request.PageSize;
        }
        #endregion helpers
    }

    public class PageMetaData
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 20;
        public int TotalCount { get; set; }
    }

}
