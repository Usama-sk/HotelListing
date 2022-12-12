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
using HotelListing.Data;

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
        [HttpGet("{id:int}" , Name="GetCity")]
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
   
        [HttpPost ]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public async Task<IActionResult> CreateCity([FromBody] CreateCityDTO cityDTO)
        {
           
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid POST Attempt in {nameof(CreateCity)} ");
                return BadRequest(ModelState);
            }
           
            try
            {
                var cities = await _unitofWork.Cities.GetAll();
                foreach (var _city in cities)
                {
                    if (_city.CountryId == cityDTO.CountryId && _city.Name == cityDTO.Name)
                    {
                        return BadRequest("City is already Exist");
                    }
                }
                var city = _mapper.Map<City>(cityDTO);
                await _unitofWork.Cities.Add(city);
                await _unitofWork.Save();
                return CreatedAtRoute("GetCity",new { id = city.Id},city);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something is went wrong in the {nameof(CreateCity)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");

            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateCity(int id, [FromBody] UpdateCityDTO cityDTO)
        {


            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogInformation($"Invalid Update Attempt in {nameof(UpdateCity)}");
                return BadRequest(ModelState);
            }
            try
            {
                var cities = await _unitofWork.Cities.GetAll();
                foreach (var _city in cities)
                {
                    if (_city.CountryId == cityDTO.CountryId && _city.Name == cityDTO.Name)
                    {
                        return BadRequest("City is already Exist");
                    }
                }
                var city = await _unitofWork.Cities.GetbyId(x=>x.Id == id );
                if (city == null)
                {
                    _logger.LogInformation($"Invalid Update Attempt in {nameof(UpdateCity)}");
                    return BadRequest("Submitted data is invalid");
                }
                _mapper.Map(cityDTO, city);
                _unitofWork.Cities.Update(city);
                await _unitofWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something is went wrong in the {nameof(UpdateCity)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");

            }
        }

    }
}
