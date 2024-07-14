using CbgTaxi24.API.Application.Services;
using CbgTaxi24.API.Data;
using CbgTaxi24.API.Workers;
using Microsoft.EntityFrameworkCore;

namespace CbgTaxi24.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

            builder.Services.AddScoped(service =>
            {               
                var configuration = service.GetRequiredService<IConfiguration>();
                return new RiderQueries(configuration.GetConnectionString("DefaultConnection")!);
            });
            
            builder.Services.AddScoped(service =>
            {               
                var configuration = service.GetRequiredService<IConfiguration>();
                return new DriverQueries(configuration.GetConnectionString("DefaultConnection")!);
            });

            builder.Services.AddScoped<RiderService>();
            builder.Services.AddScoped<DriverService>();
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
