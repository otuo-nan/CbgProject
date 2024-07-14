using CbgTaxi24.API.Data;
using CbgTaxi24.API.Database;
using CbgTaxi24.API.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CbgTaxi24.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var dbConstr = builder.Configuration.GetConnectionString("DefaultConnection")!;
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(dbConstr);
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

            builder.Services.AddScoped(service =>
            {               
                var configuration = service.GetRequiredService<IConfiguration>();
                return new RiderQueries(dbConstr);
            });
            
            builder.Services.AddScoped(service =>
            {               
                var configuration = service.GetRequiredService<IConfiguration>();
                return new DriverQueries(dbConstr);
            });

            builder.Services.AddScoped<RiderService>();
            builder.Services.AddScoped<DriverService>();
            builder.Services.AddScoped<BackOfficeService>();

            await DatabaseFunctions.AddFunctions(dbConstr);

            builder.Services.AddHostedService<DbSeeder>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
