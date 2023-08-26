using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class PokemonRepository : IPokemonRepositoryInterface
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons
                .Where((pokemon) => pokemon.Id == id)
                .FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons
                .Where((pokemon) => pokemon.Name == name)
                .FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            ICollection<Review> reviews = _context.Reviews.Where((review) => review.Pokemon.Id == pokeId).ToList();
            if(reviews.Count() <= 0)
            {
                return 0;
            }
            return (decimal) reviews.Average((review) => review.Rating);
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons
                .OrderBy((pokemon) => pokemon.Id)
                .ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemons.Any((pokemon) => pokemon.Id == pokeId);
        }
    }
}
