using AutoMapper;
using HotelListing.DTOModels;
using HotelListing.IRepository;
using HotelListing.Repository;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public CountryController(ILogger<CountryController> logger, IUnitofWork unitofWork, IMapper mapper)
            {
                _logger = logger;
                _unitofWork = unitofWork;
                _mapper = mapper;
            }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries(){

            try
            {
                var countires = await _unitofWork.Countries.GetAll();
                var results = _mapper.Map<IList<CountryDTO>>(countires);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something is went wrong in the {nameof(GetCountries)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");

            }          
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {

            try
            {
                var country = await _unitofWork.Countries.GetbyId(x =>x.Id == id,new List<string> {"Cities"});
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something is went wrong in the {nameof(GetCountry)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");

            }
        }

    }
}
