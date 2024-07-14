using Dapper;
using Microsoft.Data.SqlClient;

namespace CbgTaxi24.API.Database
{
    public static class DatabaseFunctions
    {
        public static async Task AddFunctions(string constr)
        {
            await AddCalculateDistanceAsync(constr);
        }
        static async Task AddCalculateDistanceAsync(string constr)
        {
            string _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));

            var sql = @"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CalculateDistance') AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))
                        DROP FUNCTION dbo.CalculateDistance;

                        GO
                        CREATE FUNCTION dbo.CalculateDistance (
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
                        END;

                        GO";

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql);
                Console.WriteLine("Function dbo.CalculateDistance created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating function: {ex.Message}");
            }
        }
    }
}
