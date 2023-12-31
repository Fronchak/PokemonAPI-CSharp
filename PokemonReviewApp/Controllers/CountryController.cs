﻿using AutoMapper;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDTO countryDTO)
        {
            if(countryDTO == null)
            {
                return BadRequest();
            }

            bool countryExists = _countryRepository.GetCountries()
                .Where((country) => country.Name.Trim().ToUpper() == countryDTO.Name.Trim().ToUpper())
                .Any();

            if(countryExists)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Country country = _mapper.Map<Country>(countryDTO);

            if(!_countryRepository.CreateCountry(country))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry([FromBody] CountryDTO countryDTO, [FromRoute] string countryId)
        {
            if(countryDTO == null)
            {
                return BadRequest();
            }

            int id = 0;
            try
            {
                id = int.Parse(countryId);
            }
            catch (Exception)
            {
                return NotFound();
            }

            if(!_countryRepository.CountryExists(id))
            {
                return NotFound();
            }

            bool countryExists = _countryRepository.GetCountries()
                .Where((country) => country.Name.Trim().ToUpper() == countryDTO.Name.Trim().ToUpper())
                .Where((country) => country.Id != id)
                .Any();

            if (countryExists)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ModelState);
            }

            Country country = _countryRepository.GetCountry(id);
            country.Name = countryDTO.Name;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_countryRepository.UpdateCountry(country))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
