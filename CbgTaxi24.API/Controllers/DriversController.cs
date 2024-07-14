using CbgTaxi24.API.Application.Requests;
using CbgTaxi24.API.Application.Services;

namespace CbgTaxi24.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        readonly DriverQueries _driverQueries;

        public DriversController(DriverQueries driverQueries)
        {
            _driverQueries = driverQueries;
        }

        //get driver by id
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DriverDto2))]
        public async Task<IActionResult> GetDrivers(Guid id)
        {
            return Ok(await _driverQueries.GetDriverAsync(id));
        }


        //list all available drivers
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PaginatedEntities<DriversPagingOptions,DriverDto>))]
        public async Task<IActionResult> GetDrivers([FromQuery]DriversPageRequest request)
        {
            var options = new DriversPagingOptions(request);

            options.ValidateOptions<DriverDto>();

            options.SetUpRestOfDto(await _driverQueries.GetCountOfDriversAsync(options.FilterBy, options.FilterByValue));

            return Ok(new PaginatedEntities<DriversPagingOptions, DriverDto>
            {
                Data = await _driverQueries.GetAllDriversAsync(options.PageNum - 1, options.PageSize, 
                                                options.SortField, options.SortDirection, options.FilterBy, options.FilterByValue),
                PagingOptions = options
            });
        }
        

        [HttpGet("from-location")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PaginatedEntities<DriversWithinSpecificLocationPagingOptions, DriversFromALocationDto>))]
        public async Task<IActionResult> GetDriversWithinSpecificLocation([FromQuery] DriversWithinSpecificLocationPageRequest request)
        {
            var options = new DriversWithinSpecificLocationPagingOptions(request);

            options.ValidateOptions<DriversFromALocationDto>();

            options.SetUpRestOfDto(await _driverQueries.GetCountDriversWithinSpecificLocationAsync(request.Latitude, request.Longitude, request.MaxRangeFromLocationInKm, 
                                                     options.FilterBy, options.FilterByValue));

            return Ok(new PaginatedEntities<DriversWithinSpecificLocationPagingOptions, DriversFromALocationDto>
            {
                Data = await _driverQueries.GetDriversWithinSpecificLocationAsync(request.Latitude, request.Longitude, request.MaxRangeFromLocationInKm,
                                                options.PageNum - 1, options.PageSize, 
                                                options.SortField, options.SortDirection, options.FilterBy, options.FilterByValue),
                PagingOptions = options
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TripDto))]
        public async Task<IActionResult> CompleteTrip([FromServices]DriverService service, Guid id)
        {
            return Ok(await service.CompleteTripAsync(id));
        }
    }
}
