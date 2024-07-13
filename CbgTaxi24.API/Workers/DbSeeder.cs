
using CbgTaxi24.API.Data;
using CbgTaxi24.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

#nullable disable
namespace CbgTaxi24.API.Workers
{
    public class DbSeeder(IServiceProvider serviceProvider) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await dbContext.Database.MigrateAsync(cancellationToken: cancellationToken);

            await dbContext.Riders.AddRangeAsync(GetRiders());
            await dbContext.Drivers.AddRangeAsync(GetDrivers());

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private static Rider[] GetRiders()
        {
            var data = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"SeedWork/files/riders.json"));
            return JsonSerializer.Deserialize<Rider[]>(data);
        }
        
        private static Driver[] GetDrivers()
        {
            var data = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"SeedWork/files/drivers.json"));
            return JsonSerializer.Deserialize<Driver[]>(data);
        }
    }
}
