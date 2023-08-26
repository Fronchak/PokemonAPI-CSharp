using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;

        public ReviewRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Review GetReview(int id)
        {
            return _dataContext.Reviews.Find(id);
        }

        public ICollection<Review> GetReviews()
        {
            return _dataContext.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsByPokemon(int pokeId)
        {
            return _dataContext.Reviews
                .Where((review) => review.Pokemon.Id == pokeId)
                .ToList();
        }

        public bool ReviewExists(int id)
        {
            return _dataContext.Reviews.Any((review) => review.Id == id);
        }
    }
}
