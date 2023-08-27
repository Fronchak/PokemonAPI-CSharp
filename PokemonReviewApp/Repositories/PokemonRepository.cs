using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            Owner owner = _context.Owners.Find(ownerId);
            Category category = _context.Categories.Find(categoryId);

            PokemonOwner pokemonOwner = new PokemonOwner()
            {
                Owner = owner,
                Pokemon = pokemon
            };

            PokemonCategory pokemonCategory = new PokemonCategory()
            {
                Pokemon = pokemon,
                Category = category
            };

            _context.PokemonOwners.Add(pokemonOwner);
            _context.PokemonCategories.Add(pokemonCategory);
            _context.Pokemons.Add(pokemon);
            return Save();
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

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
