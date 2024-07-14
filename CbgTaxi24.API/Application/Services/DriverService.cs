using CbgTaxi24.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CbgTaxi24.API.Application.Services
{
    public class DriverService(AppDbContext dbContext)
    {
        public async Task<TripDto> CompleteTripAsync(Guid tripId)
        {
            var trip = await dbContext.Trips.SingleOrDefaultAsync(t => t.TripId == tripId) ?? throw new PlatformException("invalid trip");
            
            trip.Status = TripStatus.Completed;

            dbContext.SaveChanges();

            return MapTrip(trip);

            static TripDto MapTrip(Trip trip)
            {
                return new TripDto
                {
                    TripId = trip.TripId,
                    FromLat = (double)trip.FromLat,
                    FromLong = (double)trip.FromLong,
                    ToLat = (double)trip.ToLat,
                    ToLong = (double)trip.ToLong,
                    Price = trip.Price,
                    RiderId = trip.RiderId,
                    DriverId = trip.DriverId,
                    Status = TripStatus.Active,
                };
            }
        }
    }
}
