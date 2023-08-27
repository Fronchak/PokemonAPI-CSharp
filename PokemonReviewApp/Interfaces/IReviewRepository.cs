using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();

        Review GetReview(int id);

        ICollection<Review> GetReviewsByPokemon(int pokeId);

        bool ReviewExists(int id);

        bool CreateReview(Review review);

        bool Save();
    }
}
