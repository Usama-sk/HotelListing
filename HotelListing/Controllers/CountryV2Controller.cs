using DataServiceLayer.DataService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    //  [ApiVersion("2.0" , Deprecated =true)]  // if we go for 3.0 version and 2.0 does not have longer support
    [ApiVersion("2.0")]
    [Route("api/country")]
    [ApiController]
    public class CountryV2Controller : ControllerBase
    {
        private AppDBContext _dbContext;

        public CountryV2Controller(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
                return Ok(_dbContext.countries);
        }
    }
}