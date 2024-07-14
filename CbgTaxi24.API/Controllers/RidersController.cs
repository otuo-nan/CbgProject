using CbgTaxi24.API.Application.Commands;

namespace CbgTaxi24.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidersController(RiderService riderService, RiderQueries riderQueries) : ControllerBase
    {
        //list all riders
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PaginatedEntities<PagingOptions, RiderDto>))]
        public async Task<IActionResult> GetRiders([FromQuery] int pageSize = 10, [FromQuery] int pageNum = 1)
        {
            return pageNum == 0 ? throw new PlatformException("pageNum cannot be zero")
                : Ok(await riderService.GetRidersAsync(pageSize, pageNum));
        }

        //get rider
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RiderDto))]
        public async Task<IActionResult> GetRider(Guid id)
        {
            return Ok(await riderService.GetRiderAsync(id));
        }
        
        [HttpGet("closest-drivers")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<DriversFromALocationDto>))]
        public async Task<IActionResult> GetClosestDrivers([FromQuery] double riderLocLatitude, [FromQuery] double riderLocLongitude, [FromQuery] int nClosestDrivers)
        {
            return Ok(await riderQueries.GetClosestDriversAsync(riderLocLatitude, riderLocLongitude, nClosestDrivers));
        }

        //Create trip request, assign to driver
        [HttpPost("request-ride")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TripDto))]
        public async Task<IActionResult> RequestRide([FromServices] IMediator mediator, RequestRideCommand command)
        {
            return Ok((await mediator.Send(command)).Entity);
        }
    }
}
