using AutoMapper;
using Infrasturcture.IRepository;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataModels.Models;
using DataModels.DTOModels;
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
        [Authorize (Roles = "Administrator")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {

           
                var countires = await _unitofWork.Countries.GetPagedList(requestParams);
                var results = _mapper.Map<IList<CountryDTO>>(countires);
                return Ok(results);
                   
        }


        [HttpGet("{id:int}", Name="GetCountry")]
        //   [ResponseCache (CacheProfileName = "120SecondsDuration")]

        //Customization caching setting override globle setting
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public,MaxAge =60)]
        [HttpCacheValidation(MustRevalidate = false)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _unitofWork.Countries.GetbyId(x =>x.Id == id,new List<string> {"Cities"});
            if(country == null)
            {
                return NotFound("Country not found");
            }
            var result = _mapper.Map<CountryDTO>(country);
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid POST Attempt in {nameof(CreateCountry)} ");
                return BadRequest(ModelState);
            }
          
           
                var countries = await _unitofWork.Countries.GetAll();
                foreach (var _country in countries)
                {
                    if (_country.Name == countryDTO.Name)
                    {
                        return BadRequest("Country is already Exist");
                    }
                }
                var country = _mapper.Map<Country>(countryDTO);
                await _unitofWork.Countries.Add(country);
                await _unitofWork.Save();
                return CreatedAtRoute("GetCity", new { id = country.Id }, country);
         
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {
            var country = await _unitofWork.Countries.GetbyId(x=>x.Id == id);
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Update Attempt in {nameof(UpdateCountry)} ");
                return BadRequest(ModelState);
            }
           
                var countries = await _unitofWork.Countries.GetAll();
                foreach (var _country in countries)
                {
                    if (_country.Name == countryDTO.Name)
                    {
                        if(_country.Id == id)
                        {
                            return Ok("Country is already Updated");
                        }
                        return BadRequest("Country is already Exist");
                    }
                }
                _mapper.Map(countryDTO,country);
                _unitofWork.Countries.Update(country);
                await _unitofWork.Save();
                return NoContent();
           
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id<1)
            {
                _logger.LogInformation($"Invalid Delete Attempt in {nameof(DeleteCountry)}");
                return BadRequest();
            }
            
                var country = await _unitofWork.Countries.GetbyId(x => x.Id == id);
                if (country == null)
                {
                    _logger.LogInformation($"Invalid Delete Attempt in {nameof(DeleteCountry)}");
                    return BadRequest("Submitted data is invalid");
                }
                await _unitofWork.Countries.Delete(id);
                await _unitofWork.Save();
                return NoContent();
           
          
        }


    }
}
