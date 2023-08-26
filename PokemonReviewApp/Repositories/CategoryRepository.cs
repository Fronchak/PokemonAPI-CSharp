using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool CategoryExists(int id)
        {
            return _dataContext.Categories.Any((category) => category.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _dataContext.Categories
                .OrderBy((category) => category.Name)
                .ToList();
        }

        public Category GetCategory(int id)
        {
            return _dataContext.Categories.Find(id);
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _dataContext.PokemonCategories
                .Where((pc) => pc.CategoryId == categoryId)
                .Select((pc) => pc.Pokemon)
                .ToList();
        }
    }
}
