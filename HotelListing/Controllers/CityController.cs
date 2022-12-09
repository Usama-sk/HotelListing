using AutoMapper;
using HotelListing.DTOModels;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace HotelListing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public CityController(ILogger<CountryController> logger, IUnitofWork unitofWork, IMapper mapper)
        {
            _logger = logger;
            _unitofWork = unitofWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCities()
        {
            try
            {
                var cities = await _unitofWork.Cities.GetAll();
                var results = _mapper.Map<IList<CityDTO>>(cities);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something is went wrong in the {nameof(GetCities)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");

            }
        }
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCity(int id)
        {

            try
            {
                var city = await _unitofWork.Cities.GetbyId(x => x.Id == id, new List<string> { "Country" });
                var result = _mapper.Map<CityDTO>(city);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something is went wrong in the {nameof(GetCity)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");

            }
        }

    }
}
