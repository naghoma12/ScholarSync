using Microsoft.EntityFrameworkCore;
using ScholarSyncMVC.Data;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;

namespace ScholarSyncMVC.Repository
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly ScholarSyncConext _conext;

        public ReviewRepository(ScholarSyncConext conext):base(conext) 
        {
            _conext = conext;
        }
        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return _conext.Reviews.Include(x => x.User).Where(x => x.IsDeleted == false && x.User.DepartmentId != null && x.User.PhotoURL != null).ToList();
        }
    }
}
