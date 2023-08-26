using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Security.Cryptography.X509Certificates;

namespace PokemonReviewApp.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDTO>))]
        public IActionResult GetCategories()
        {
            ICollection<Category> categories = _categoryRepository.GetCategories();
            ICollection<CategoryDTO> categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categoryDTOs);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)
        {
            if(!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            Category category = _categoryRepository.GetCategory(categoryId);
            CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(category);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(categoryDTO);
        }

        [HttpGet("{categoryId}/pokemons")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDTO>))]
        public IActionResult GetPokemonsByCategory(int categoryId)
        {
            ICollection<Pokemon> pokemons = _categoryRepository.GetPokemonByCategory(categoryId);
            ICollection<PokemonDTO> pokemonDTOs = _mapper.Map<List<PokemonDTO>>(pokemons);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(pokemonDTOs);
        }
    }
}
