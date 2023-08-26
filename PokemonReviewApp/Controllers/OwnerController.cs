using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDTO>))]
        public IActionResult GetOwners()
        {
            ICollection<Owner> owners = _ownerRepository.GetOwners();
            ICollection<OwnerDTO> ownerDTOs = _mapper.Map<List<OwnerDTO>>(owners);

            return Ok(ownerDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(OwnerDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int id)
        {
            if(!_ownerRepository.OwnerExists(id))
            {
                return NotFound();
            }

            Owner owner = _ownerRepository.GetOwner(id);
            OwnerDTO ownerDTO = _mapper.Map<OwnerDTO>(owner);

            return Ok(ownerDTO);
        }

        [HttpGet("pokemons")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDTO>))]
        public IActionResult GetPokemonsByOwner(int ownerId)
        {
            ICollection<Pokemon> pokemons = _ownerRepository.GetPokemonsByOwner(ownerId);
            ICollection<PokemonDTO> pokemonDTOs = _mapper.Map<List<PokemonDTO>>(pokemons);

            return Ok(pokemonDTOs);
        }

        [HttpGet("pokemons/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDTO>))]
        public IActionResult GetPokemonOwners(int pokeId)
        {
            ICollection<Owner> owners = _ownerRepository.GetPokemonOwners(pokeId);
            ICollection<OwnerDTO> ownerDTOs = _mapper.Map<List<OwnerDTO>>(owners);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(ownerDTOs);
        }
    }
}
