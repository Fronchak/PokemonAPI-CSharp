using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _dataContext;

        public OwnerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public ICollection<Owner> GetOwners()
        {
            return _dataContext.Owners
                .OrderBy((owner) => owner.FirstName)
                .ToList();
        }

        public Owner GetOwner(int id)
        {
            return _dataContext.Owners.Find(id);
        }

        public ICollection<Owner> GetPokemonOwners(int pokeId)
        {
            return _dataContext.PokemonOwners
                .Where((po) => po.PokemonId == pokeId)
                .Select((po) => po.Owner)
                .ToList();
        }

        public ICollection<Pokemon> GetPokemonsByOwner(int ownerId)
        {
            return _dataContext.PokemonOwners
                .Where((po) => po.OwnerId == ownerId)
                .Select((po) => po.Pokemon)
                .ToList();
        }

        public bool OwnerExists(int id)
        {
            return _dataContext.Owners
                .Any((owner) => owner.Id == id);
        }
    }
}
