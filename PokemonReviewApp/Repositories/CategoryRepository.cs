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

        public bool CreateCategory(Category category)
        {
            _dataContext.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _dataContext.Categories.Remove(category);
            return Save();
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

        public bool Save()
        {
            int saved = _dataContext.SaveChanges();
            return saved > 0;
        }

        public bool UpdateCategory(Category category)
        {
            _dataContext.Categories.Update(category);
            return Save();
        }
    }
}
