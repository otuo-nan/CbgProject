﻿using CbgTaxi24.API.Models;
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

            if (!await dbContext.Riders.AnyAsync(cancellationToken: cancellationToken)) 
            {
                await dbContext.Riders.AddRangeAsync(GetRiders());
                await dbContext.Drivers.AddRangeAsync(GetDrivers());

                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private static Rider[] GetRiders()
        {
            var data = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"Utility/Files/riders.json"));
            return JsonSerializer.Deserialize<Rider[]>(data);
        }
        
        private static Driver[] GetDrivers()
        {
            var data = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"Utility/Files/drivers.json"));
            return JsonSerializer.Deserialize<Driver[]>(data);
        }
    }
}
