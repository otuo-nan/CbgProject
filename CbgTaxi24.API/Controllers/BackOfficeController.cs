namespace CbgTaxi24.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackOfficeController(BackOfficeService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTrips([FromQuery] TripFilter filter,[FromQuery]int pageSize = 10, [FromQuery] int pageNum = 1)
        {
            return Ok(await (service.GetTripsAsync(pageSize, pageNum, filter)));
        }
    }
}
