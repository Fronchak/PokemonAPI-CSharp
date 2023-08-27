using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/pokemons")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRespository, 
            ICategoryRepository categoryRepository,
            IOwnerRepository ownerRepository,
            IMapper mapper)
        {
            _pokemonRepository = pokemonRespository;
            _categoryRepository = categoryRepository;
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDTO>))]
        public IActionResult GetPokemons()
        {
            ICollection<Pokemon> pokemons = _pokemonRepository.GetPokemons();
            ICollection<PokemonDTO> dtos = _mapper.Map<List<PokemonDTO>>(pokemons);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(dtos);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type=typeof(PokemonDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if(!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }
            Pokemon pokemon = _pokemonRepository.GetPokemon(pokeId);
            PokemonDTO dto = _mapper.Map<PokemonDTO>(pokemon);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dto);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if(!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }
            decimal rating = _pokemonRepository.GetPokemonRating(pokeId);
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromBody] PokemonInsertDTO pokemonInsertDTO)
        {
            if(pokemonInsertDTO == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(!_categoryRepository.CategoryExists(pokemonInsertDTO.CategoryId))
            {
                return NotFound("Category not found");
            }

            if(!_ownerRepository.OwnerExists(pokemonInsertDTO.OwnerId))
            {
                return NotFound("Owner not found");
            }

            Pokemon pokemon = _mapper.Map<Pokemon>(pokemonInsertDTO);
            if (!_pokemonRepository.CreatePokemon(pokemonInsertDTO.OwnerId, pokemonInsertDTO.CategoryId, pokemon))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
    }
}
