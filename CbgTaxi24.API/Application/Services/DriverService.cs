using CbgTaxi24.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;

namespace CbgTaxi24.API.Application.Services
{
    public class DriverService(AppDbContext dbContext)
    {
        public async Task<InvoiceDto> CompleteTripAsync(Guid tripId)
        {


            using IDbContextTransaction transaction = dbContext.Database.BeginTransaction();
            try
            {
                var trip = await dbContext.Trips.Include(t => t.Rider)
                                           .Include(t => t.Driver)
                                           .SingleOrDefaultAsync(t => t.TripId == tripId) ?? throw new PlatformException("invalid trip");

                trip.Status = TripStatus.Completed;
                trip.Rider.IsInTrip = false;
                trip.Driver.Status = DriverStatus.Available;

                var invoice = new Invoice
                {
                    Price = trip.Price,
                    RiderId = trip.RiderId,
                    TripId = tripId,
                };

                await dbContext.Invoices.AddAsync(invoice);

                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return MapTripToInvoice(invoice.InvoiceId, trip);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new PlatformException("failed to complete trip", ex.InnerException!);
            }
        }


        static InvoiceDto MapTripToInvoice(Guid invoiceId, Trip trip)
        {
            var tripMetaData = JsonSerializer.Deserialize<TripMetaData>(trip.Metadata)!;

            return new InvoiceDto
            {
                InvoiceId = invoiceId,
                RiderName = trip.Rider.FirstName,
                DriverName = trip.Driver.Name,
                LicensePlate = trip.Driver.CarNumber,
                Price = (double)trip.Price,

                FromLat = tripMetaData.FromLocation.Latitude,
                FromLong = tripMetaData.FromLocation.Longitude,
                FromName = tripMetaData.FromLocation?.Name,

                ToLat = tripMetaData.ToLocation.Latitude,
                ToLong = tripMetaData.ToLocation.Longitude,
                ToName = tripMetaData.ToLocation?.Name,
            };
        }

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
                Status = trip.Status,
            };
        }

        public async Task<TripDto2?> GetDriverActiveTripAsync(Guid id)
        {
            var trip = await dbContext.Trips.Include(t => t.Driver)
                                    .Include(t => t.Rider)
                                    .FirstOrDefaultAsync(t => t.Status == Models.TripStatus.Active && t.DriverId == id);

            if (trip != null)
            {
                return TripDto2.MapTrip(trip);
            }

            throw new PlatformException("no active trip found");
        }
    }
}
