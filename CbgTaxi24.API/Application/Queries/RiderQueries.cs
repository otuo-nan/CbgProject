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

        public async Task<IEnumerable<RiderDto>> GetAllRidersAsync(int skip, int pageSize, string orderBy, OrderDirection orderDirection)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var result = await connection.QueryAsync<dynamic>(
                  @$"SELECT r.RiderId, r.FirstName, r.Phone, r.CarNumber, r.ServiceType, r.Status, r.Rating, l.Latitude, l.Longitude, l.Region, l.Name
	                    FROM Riders r JOIN Locations l 
	                    ON r.LocationId = l.LocationId
	                    ORDER BY @orderBy {(orderDirection == OrderDirection.ASC ? "ASC" : "DESC")}
                        OFFSET @skip ROWS
                        FETCH NEXT @pageSize ROWS ONLY", new { orderBy, skip, pageSize }
                );

            if (result.AsList().Count == 0)
                return new List<RiderDto>();

            return [];
        }

        //private IEnumerable<DriverDto> MapDrivers(dynamic result)
        //{
        //    List<DriverDto> drivers = [];

        //    foreach (dynamic driver in result)
        //    {
        //        drivers.Add(new DriverDto
        //        {
        //            DriverId = driver.DriverId,
        //            Name = driver.Name,
        //        });
        //    }
        //}
    }
}
