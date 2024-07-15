using CbgTaxi24.API.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CbgTaxi24.API.Application.Services
{
    public class RiderService(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<PaginatedEntities<PagingOptions, RiderDto>> GetRidersAsync(int pageSize = 10, int pageNum = 1)
        {
            var totalRiders = await _dbContext.Riders.LongCountAsync();

            var riders = await _dbContext.Riders.AsNoTracking()
                                         .Include(r => r.Location)
                                         .OrderBy(r => r.FirstName)
                                         .Skip((pageNum - 1) * pageSize)
                                         .Take(pageSize)
                                         .Select(r => new RiderDto
                                         {
                                             RiderId = r.RiderId,
                                             FirstName = r.FirstName,
                                             LastName = r.LastName,
                                             Latitude = (double)r.Location.Latitude,
                                             Longitude = (double)r.Location.Longitude,
                                             LocationName = r.Location.Name
                                         })
                                         .ToListAsync();

            var options = new PagingOptions { PageSize = pageSize, PageNum = pageNum };
            options.SetUpRestOfDto(totalRiders);

            return new PaginatedEntities<PagingOptions, RiderDto>
            {
                PagingOptions = options,
                Data = riders
            };
        }

        public async Task<RiderDto> GetRiderAsync(Guid id)
        {
           return await _dbContext.Riders.Include(r => r.Location)
                                    .Where(r => r.RiderId == id)
                                    .Select(r => new RiderDto
                                    {
                                        RiderId = r.RiderId,
                                        FirstName = r.FirstName,
                                        LastName = r.LastName,
                                        Latitude = (double)r.Location.Latitude,
                                        Longitude = (double)r.Location.Longitude,
                                        LocationName = r.Location.Name,
                                        IsInTrip= r.IsInTrip
                                    })
                                    .FirstOrDefaultAsync() ?? throw new PlatformException("not found");
        }

        public async Task<TripDto2?> GetRiderActiveTripAsync(Guid id)
        {
            var trip = await _dbContext.Trips.Include(t => t.Driver)
                                    .Include(t => t.Rider)
                                    .FirstOrDefaultAsync(t => t.Status == Models.TripStatus.Active && t.RiderId == id);

            if (trip != null)
            {
                return TripDto2.MapTrip(trip);
            }

            throw new PlatformException("no active trip found");
        }
    }
}
