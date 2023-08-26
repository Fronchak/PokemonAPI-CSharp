using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _dataContext;

        public ReviewerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Reviewer GetReviewer(int id)
        {
            return _dataContext.Reviewers.Find(id);
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _dataContext.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _dataContext.Reviews
                .Where((review) => review.Reviewer.Id == reviewerId)
                .ToList();
        }

        public bool ReviewerExists(int id)
        {
            return _dataContext.Reviewers.Any((reviewer) => reviewer.Id == id);
        }
    }
}
