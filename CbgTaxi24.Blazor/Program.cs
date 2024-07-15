using CbgTaxi24.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CbgTaxi24.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            AddHttpServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();


            void AddHttpServices()
            {
                builder.Services.AddHttpClient<RiderService>(client =>
                {
                    client.BaseAddress = new Uri(builder.Configuration["APIEndpointBaseUrl"]!);
                });

                builder.Services.AddHttpClient<DriverService>(client =>
                {
                    client.BaseAddress = new Uri(builder.Configuration["APIEndpointBaseUrl"]!);
                });

                builder.Services.AddHttpClient<BackOfficeService>(client =>
                {
                    client.BaseAddress = new Uri(builder.Configuration["APIEndpointBaseUrl"]!);
                });
            }
        }
    }
}
