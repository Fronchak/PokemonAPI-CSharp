using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.Controllers
{
    [Route("api/reviewers")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDTO>))]
        public IActionResult GetReviewers()
        {
            ICollection<Reviewer> reviewers = _reviewerRepository.GetReviewers();
            ICollection<ReviewerDTO> reviewerDTOs = _mapper.Map<List<ReviewerDTO>>(reviewers);

            return Ok(reviewerDTOs);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(ReviewerDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if(!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }

            Reviewer reviewer = _reviewerRepository.GetReviewer(reviewerId);
            ReviewerDTO reviewerDTO = _mapper.Map<ReviewerDTO>(reviewer);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(reviewerDTO);
        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            ICollection<Review> reviews = _reviewerRepository.GetReviewsByReviewer(reviewerId);
            ICollection<ReviewDTO> reviewDTOs = _mapper.Map<List<ReviewDTO>>(reviews);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(reviewDTOs);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDTO reviewerDTO)
        {
            if(reviewerDTO == null)
            {
                return BadRequest();
            }

            Reviewer reviewer = _mapper.Map<Reviewer>(reviewerDTO);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(!_reviewerRepository.CreateReviewer(reviewer))
            {
                ModelState.AddModelError("", "Something went wrong when saving");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
    }
}
