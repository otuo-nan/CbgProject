using CbgTaxi24.API.Application.Queries;
using CbgTaxi24.API.Application.Queries.Dtos;
using CbgTaxi24.API.Application.Services;
using CbgTaxi24.API.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CbgTaxi24.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidersController : ControllerBase
    {
        readonly RiderService _riderService;
        readonly RiderQueries _riderQueries;
        public RidersController(RiderService riderService, RiderQueries riderQueries)
        {
            _riderService = riderService;
            _riderQueries = riderQueries;
        }

        //list all riders
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PaginatedEntities<PagingOptions, RiderDto>))]
        public async Task<IActionResult> GetRiders([FromQuery] int pageSize = 10, [FromQuery] int pageNum = 1)
        {
            return pageNum == 0 ? throw new PlatformException("pageNum cannot be zero")
                : Ok(await _riderService.GetRidersAsync(pageSize, pageNum));
        }

        //get rider
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RiderDto))]
        public async Task<IActionResult> GetRider(Guid id)
        {
            return Ok(await _riderService.GetRiderAsync(id));
        }
        
        [HttpGet("closest-drivers")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<DriversFromALocationDto>))]
        public async Task<IActionResult> GetClosstDrivers([FromQuery] decimal riderLocLatitude, [FromQuery] decimal riderLocLongitude, [FromQuery] int nClosestDrivers)
        {
            return Ok(await _riderQueries.GetClosestDriversAsync(riderLocLatitude, riderLocLongitude, nClosestDrivers));
        }

        //Create trip request, assign to driver
    }
}
