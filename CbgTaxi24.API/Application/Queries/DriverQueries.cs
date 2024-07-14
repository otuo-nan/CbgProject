using CbgTaxi24.API.Application.Queries.Dtos;
using CbgTaxi24.API.Infrastructure.Exceptions;
using CbgTaxi24.API.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CbgTaxi24.API.Application.Queries
{
    public class DriverQueries(string constr)
    {
        readonly string _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));

        public async Task<DriverDto2> GetDriverAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            return await connection.QuerySingleOrDefaultAsync<DriverDto2>(
                @$"SELECT d.DriverId, d.Name, d.Phone, d.CarNumber, d.ServiceType, d.Status, d.Rating, l.Latitude, l.Longitude, l.Region, l.Name AS LocationName
	                    FROM Drivers d JOIN Locations l 
	                    ON d.LocationId = l.LocationId
                        WHERE DriverId = @id", new { id} ) ?? throw new PlatformException("not found");
        }
        
        public async Task<int> GetCountOfDriversAsync(FilterDriversBy? filterBy, string? filterByValue)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            return await connection.ExecuteScalarAsync<int>($"SELECT Count(*) FROM Drivers {GetSqlFilterComponent(filterBy, filterByValue)}");
        }

        public async Task<List<DriverDto>> GetAllDriversAsync(int skip, int pageSize, string? orderBy, string orderDirection, FilterDriversBy? filterBy, string? filterByValue)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? nameof(DriverDto.Name) : orderBy;

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var result = await connection.QueryAsync<dynamic>(
                  @$"SELECT d.DriverId, d.Name, d.Phone, d.CarNumber, d.ServiceType, d.Status, d.Rating, l.Latitude, l.Longitude, l.Region, l.Name AS LocationName
	                    FROM Drivers d JOIN Locations l 
	                    ON d.LocationId = l.LocationId {GetSqlFilterComponent(filterBy, filterByValue)}
	                    ORDER BY {orderBy} {orderDirection} 
                        OFFSET @skip ROWS
                        FETCH NEXT @pageSize ROWS ONLY", new { skip, pageSize });

            if (!result.Any())
                return [];

            return MapDrivers(result);
        }

        public async Task<int> GetCountDriversWithinSpecificLocationAsync(decimal locLatitude, decimal locLongitude, float maxRangeFromLocation, 
            FilterDriversBy? filterBy, string? filterByValue)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            return await connection.ExecuteScalarAsync<int>(
                @$"WITH CTE AS (Select ROUND(dbo.CalculateDistance(@locLatitude, @locLongitude, l.Latitude, l.Longitude), 2) AS Distance
	                    FROM Drivers d JOIN Locations l
	                    ON d.LocationId = l.LocationId {GetSqlFilterComponent(filterBy, filterByValue)})
                      SELECT Count(*) FROM CTE 
                        WHERE Distance < @maxRangeFromLocation", new { locLatitude, locLongitude, maxRangeFromLocation});
        }

        public async Task<List<DriversFromALocationDto>> GetDriversWithinSpecificLocationAsync(decimal locLatitude, decimal locLongitude, float maxRangeFromLocation,
            int skip, int pageSize, string? orderBy, string orderDirection, 
            FilterDriversBy? filterBy, string? filterByValue)
        {
            orderBy = string.IsNullOrEmpty(orderBy) ? nameof(DriversFromALocationDto.Name) : orderBy;

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var result = await connection.QueryAsync<DriversFromALocationDto>(
                  @$"WITH CTE AS (Select  d.DriverId, d.Name, d.Phone, d.CarNumber, d.ServiceType, d.Status, d.Rating, l.Latitude, l.Longitude, l.Region, l.Name AS LocationName,
                        ROUND(dbo.CalculateDistance(@locLatitude, @locLongitude, l.Latitude, l.Longitude), 2) AS Distance
	                    FROM Drivers d JOIN Locations l
	                    ON d.LocationId = l.LocationId {GetSqlFilterComponent(filterBy, filterByValue)})
                      SELECT * FROM CTE
                        WHERE Distance < @maxRangeFromLocation
	                    ORDER BY {orderBy} {orderDirection} 
                        OFFSET @skip ROWS
                        FETCH NEXT @pageSize ROWS ONLY", new { locLatitude, locLongitude, maxRangeFromLocation, skip, pageSize });

            if (!result.Any())
                return [];

            return result.AsList();
        }

        static string GetSqlFilterComponent(FilterDriversBy? filterBy, string? filterByValue)
        {
            if (filterBy == null)
            {
                return string.Empty;
            }
            else
            {
                if (string.IsNullOrEmpty(filterByValue))
                {
                    throw new PlatformException("provide a filterByValue for filter");
                }

                switch (filterBy)
                {
                    case FilterDriversBy.ServiceType:
                        {
                            return $"WHERE ServiceType={(int)Enum.Parse<ServiceType>(filterByValue, ignoreCase: true)} ";
                        }
                    case FilterDriversBy.DriverStatus:
                        {
                            return $"WHERE Status={(int)Enum.Parse<DriverStatus>(filterByValue, ignoreCase: true)} ";
                        }
                    default:
                        throw new PlatformException("invalid filter");
                }
            }
        }

        private static List<DriverDto> MapDrivers(dynamic result)
        {
            List<DriverDto> drivers = [];

            foreach (dynamic driver in result)
            {
                drivers.Add(new DriverDto
                {
                    DriverId = driver.DriverId,
                    Name = driver.Name,
                    Phone = driver.Phone,
                    CarNumber = driver.CarNumber,
                    ServiceType = (ServiceType)driver.ServiceType,
                    Status = (DriverStatus)driver.Status,
                    Rating = driver.Rating,
                    Location = new LocationDto
                    {
                        Latitude = driver.Latitude,
                        Longitude = driver.Longitude,
                        //Region = driver.Region,
                        Name = driver.LocationName,
                    }
                });
            }

            return drivers;
        }
    }
}
