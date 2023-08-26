using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDTO>))]
        public IActionResult GetCountries()
        {
            ICollection<Country> countries = _countryRepository.GetCountries();
            ICollection<CountryDTO> countryDTOs = _mapper.Map<List<CountryDTO>>(countries);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(countryDTOs);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if(!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            Country country = _countryRepository.GetCountry(countryId);
            CountryDTO countryDTO = _mapper.Map<CountryDTO>(country);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(countryDTO);
        }

        [HttpGet("owners/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            Country country = _countryRepository.GetCountryByOwner(ownerId);
            CountryDTO countryDTO = _mapper.Map<CountryDTO>(country);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(countryDTO);
        }
    }
}
