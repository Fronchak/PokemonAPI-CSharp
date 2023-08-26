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
        private readonly IPokemonRepositoryInterface _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepositoryInterface pokemonRespository, 
            IMapper mapper)
        {
            _pokemonRepository = pokemonRespository;
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

    }
}
