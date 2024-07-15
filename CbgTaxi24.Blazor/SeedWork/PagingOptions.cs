namespace CbgTaxi24.Blazor.SeedWork
{
#nullable disable
    public class PagingOptions
    {
        public string CurrentSortField { get; set; }
        public string CurrentSortDirection { get; set; }

        public static void SetUpMetaData(PageMetaData metadata, int queryCount = 0)
        {
            metadata.TotalCount = queryCount;

            if (metadata.TotalCount > 0)
                metadata.TotalPages = (int)Math.Ceiling((double)metadata.TotalCount / metadata.PageSize);

            //used if PageNum posted value is greater than the calculated number of pages
            metadata.CurrentPage = metadata.TotalCount == 0 ? 1 : Math.Min(Math.Max(1, metadata.CurrentPage), metadata.TotalPages);
        }

        public static void SetUpMetaData(PageMetaData metadata)
        {
            if (metadata.TotalCount > 0)
                metadata.TotalPages = (int)Math.Ceiling((double)metadata.TotalCount / metadata.PageSize);

            //used if PageNum posted value is greater than the calculated number of pages
            metadata.CurrentPage = metadata.TotalCount == 0 ? 1 : Math.Min(Math.Max(1, metadata.CurrentPage), metadata.TotalPages);
        }
    }

    public class PageMetaData
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 20;
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public void Reset()
        {
            CurrentPage = 1;
            TotalCount = 0;
            TotalPages = 0;
        }
    }

    public class PagingLink
    {
        public string Text { get; set; }
        public int Page { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }

        public PagingLink(int page, bool enabled, string text)
        {
            Page = page;
            Enabled = enabled;
            Text = text;
        }
    }

    /// <summary>
    /// specific class used for getting paged data from other microservices
    /// </summary>
    public class PagedData<TData>
    {
        public PagingParams PagingOptions { get; set; }
        public IEnumerable<TData> Data { get; set; }

    }

    public class PagingParams
    {
        public string Status { get; set; }
        public string Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int DataSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
