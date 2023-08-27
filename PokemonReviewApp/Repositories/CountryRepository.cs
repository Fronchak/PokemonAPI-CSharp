using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _dataContext;

        public CountryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CountryExists(int id)
        {
            return _dataContext.Countries.Any((country) => country.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _dataContext.Countries.Add(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _dataContext.Countries
                .OrderBy((country) => country.Name)
                .ToList();
        }

        public Country GetCountry(int id)
        {
            return _dataContext.Countries.Find(id);
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _dataContext.Owners
                .Where((owner) => owner.Id == ownerId)
                .Select((owner) => owner.Country)
                .FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return _dataContext.Owners
                .Where((owner) => owner.Country.Id == countryId)
                .ToList();
        }

        public bool Save()
        {
            int saved = _dataContext.SaveChanges();
            return saved > 0;
        }

        public bool UpdateCountry(Country country)
        {
            _dataContext.Countries.Update(country);
            return Save();
        }
    }
}
