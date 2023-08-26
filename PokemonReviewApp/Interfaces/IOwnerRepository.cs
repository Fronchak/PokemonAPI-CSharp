using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int id);

        ICollection<Owner> GetPokemonOwners(int pokeId);

        ICollection<Pokemon> GetPokemonsByOwner(int ownerId);

        bool OwnerExists(int id);
    }
}
