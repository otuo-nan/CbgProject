using CbgTaxi24.API.Application.SeedWork;
using CbgTaxi24.API.Infrastructure.Exceptions;
using CbgTaxi24.API.Models;
using CbgTaxi24.API.Utility;
using Microsoft.EntityFrameworkCore;

namespace CbgTaxi24.API.Application.Commands
{
    public class RequestRideCommand : IRequest<HandlerResponse<TripDto>>
    {
        public Guid RiderId { get; set; }
        public double CurrentLatitude { get; set; }
        public double CurrentLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }

        public double Distance => double.Round(DistanceCalculator.GetDistance(CurrentLatitude, CurrentLongitude, DestinationLatitude, DestinationLongitude) / 1000, 2);
    }

    public class RequestRideCommandHandler : IRequestHandler<RequestRideCommand, HandlerResponse<TripDto>>
    {
        readonly ILogger<RequestRideCommandHandler> _logger;
        readonly AppDbContext _dbContext;
        readonly DriverQueries _driverQueries;

        public RequestRideCommandHandler(ILogger<RequestRideCommandHandler> logger,
            AppDbContext dbContext, DriverQueries driverQueries)
        {
            _logger = logger;
            _dbContext = dbContext;
            _driverQueries = driverQueries;
        }

        public async Task<HandlerResponse<TripDto>> Handle(RequestRideCommand request, CancellationToken cancellationToken)
        {
            var driver = await _driverQueries.GetClosestDriverAsync(request.CurrentLatitude, request.CurrentLongitude) ?? throw new PlatformException("no drivers available");

            if (await _dbContext.Riders.AnyAsync(r => r.RiderId == request.RiderId, cancellationToken: cancellationToken))
            {
                var trip = new Trip
                {
                    FromLat = (decimal)request.CurrentLatitude,
                    FromLong = (decimal)request.CurrentLongitude,
                    ToLat = (decimal)request.DestinationLatitude,
                    ToLong = (decimal)request.DestinationLongitude,
                    Status = TripStatus.Active,
                    Price = (decimal)TripPriceCalculator.GetPrice(request.Distance),
                    RiderId = request.RiderId,
                    DriverId = driver.DriverId
                };

                await _dbContext.Trips.AddAsync(trip, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);
                return new HandlerResponse<TripDto>(MapTrip(trip));
            }

            throw new PlatformException("invalid riderId");

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
