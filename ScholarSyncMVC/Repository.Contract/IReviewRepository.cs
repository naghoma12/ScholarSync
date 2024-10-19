using ScholarSyncMVC.Models;

namespace ScholarSyncMVC.Repository.Contract
{
    public interface IReviewRepository :IGenericRepository<Review>
    {
        Task<IEnumerable<Review>> GetAllReviews();
    }
}
