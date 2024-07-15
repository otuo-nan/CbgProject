using CbgTaxi24.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CbgTaxi24.API.Application.Services
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TripFilter : byte
    {
        Active = 1, Completed = 2, All = 3
    }

    public class BackOfficeService(AppDbContext dbContext)
    {
        public async Task<PaginatedEntities<PagingOptions, TripDto2>> GetTripsAsync(int pageSize = 10, int pageNum = 1, TripFilter filter = TripFilter.All)
        {
            var totalRiders = await dbContext.Trips.LongCountAsync();

            var trips1 = dbContext.Trips.AsNoTracking();

            IQueryable<Trip> trips2 = filter switch
            {
                TripFilter.Active => trips1.Where(t => t.Status == TripStatus.Active),
                TripFilter.Completed => trips1.Where(t => t.Status == TripStatus.Completed),
                TripFilter.All => trips1,
                _ => throw new ArgumentException("invalid trip filter"),
            };

            var trips3 = await trips2.Include(t => t.Driver)
                                    .Include(t => t.Rider)
                                    .OrderByDescending(r => r.CreatedOn)
                                    .Skip((pageNum - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            var options = new PagingOptions { PageSize = pageSize, PageNum = pageNum };
            options.SetUpRestOfDto(totalRiders);

            return new PaginatedEntities<PagingOptions, TripDto2>
            {
                PagingOptions = options,
                Data = trips3.Select(t => TripDto2.MapTrip(t))
            };
        }
    }
}
