using CbgTaxi24.API.Application.Queries.Dtos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CbgTaxi24.API.Application.Queries
{
    public enum OrderDirection : byte
    {
        ASC = 1, DESC = 2
    }

    public class RiderQueries(string constr)
    {
        readonly string _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));

        public async Task<IEnumerable<DriversFromALocationDto>> GetClosestDriversAsync(double riderLocLatitude, double riderLocLongitude, int nClosestDrivers)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var result = await connection.QueryAsync<DriversFromALocationDto>(
                  @$"Select TOP(@nClosestDrivers) d.DriverId, d.Name, d.Phone, d.CarNumber, d.ServiceType, d.Status, d.Rating, l.Latitude, l.Longitude, l.Region, l.Name AS LocationName,
                        ROUND(dbo.CalculateDistance(@riderLocLatitude, @riderLocLongitude, l.Latitude, l.Longitude), 2) AS Distance
	                    FROM Drivers d JOIN Locations l
	                    ON d.LocationId = l.LocationId 
                        ORDER BY Distance", new { riderLocLatitude, riderLocLongitude, nClosestDrivers }
                );

            if (!result.Any())
                return [];

            return result;
        }
    }
}
