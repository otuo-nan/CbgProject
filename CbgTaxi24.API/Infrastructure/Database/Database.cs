using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CbgTaxi24.API.Infrastructure.Database
{
    public static class Database
    {
        public static WebApplication UseMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetService<ILogger<Program>>();

            logger!.LogInformation("db migrations start");
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            db.Database.Migrate();

            AddFunctions(app.Configuration.GetConnectionString("DefaultConnection")!);

            logger!.LogInformation("db migrations end");

            return app;
        }

        public static void AddFunctions(string constr)
        {
            AddCalculateDistance(constr);
        }

        static void AddCalculateDistance(string constr)
        {
            string _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));

            var sql1 = @"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CalculateDistance') AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))
                        DROP FUNCTION dbo.CalculateDistance;";

            var sql2 = @"CREATE FUNCTION dbo.CalculateDistance (
                            @lat1 FLOAT,
                            @lon1 FLOAT,
                            @lat2 FLOAT,
                            @lon2 FLOAT
                        )
                        RETURNS FLOAT
                        AS
                        BEGIN
                            DECLARE @radius FLOAT = 6371; -- Earth's radius in kilometers

                            DECLARE @dLat FLOAT = RADIANS(@lat2 - @lat1);
                            DECLARE @dLon FLOAT = RADIANS(@lon2 - @lon1);

                            DECLARE @a FLOAT = SIN(@dLat / 2) * SIN(@dLat / 2) +
                                              COS(RADIANS(@lat1)) * COS(RADIANS(@lat2)) *
                                              SIN(@dLon / 2) * SIN(@dLon / 2);

                            DECLARE @c FLOAT = 2 * ATN2(SQRT(@a), SQRT(1 - @a));

                            DECLARE @distance FLOAT = @radius * @c;

                            RETURN @distance;
                        END;";

            try
            {
                using var connection = new SqlConnection(_connectionString);
                //connection.Execute(sql1);
                connection.Execute(sql2);
                Console.WriteLine("Function dbo.CalculateDistance created successfully.");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error creating function: {ex.Message}");
            }
        }
    }
}
