using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        public IActionResult GetReviews()
        {
            ICollection<Review> reviews = _reviewRepository.GetReviews();
            ICollection<ReviewDTO> reviewDTOs = _mapper.Map<List<ReviewDTO>>(reviews);

            return Ok(reviewDTOs);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if(!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            Review review = _reviewRepository.GetReview(reviewId);
            ReviewDTO reviewDTO = _mapper.Map<ReviewDTO>(review);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(reviewDTO);
        }

        [HttpGet("pokemons/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByPokemon(int pokeId)
        {
            ICollection<Review> reviews = _reviewRepository.GetReviewsByPokemon(pokeId);
            ICollection<ReviewDTO> reviewDTOs = _mapper.Map<List<ReviewDTO>>(reviews);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(reviewDTOs);
        }
    }
}
