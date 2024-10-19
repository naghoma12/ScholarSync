using Microsoft.EntityFrameworkCore;
using ScholarSyncMVC.Data;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;

namespace ScholarSyncMVC.Repository
{
    public class ScholarshipRepository : GenericRepository<Scholarship>, IScholarship
    {
        private readonly ScholarSyncConext _conext;

        public ScholarshipRepository(ScholarSyncConext conext):base(conext) 
        {
            _conext = conext;
        }
        public async Task<IEnumerable<Scholarship>> GetAllWithTables()
        {
            return await _conext.Scholarships.Include("Category").Include("University").Include("Country").Include("Department").Where(x => x.IsDeleted == false).ToListAsync();
            
        }

        public async Task<Scholarship?> GetByIdInclude(int id)
        {
            return _conext.Scholarships.Include("Category").Include("University").Include("Country").Include("Department").Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<Scholarship>> GetAllInDept(int DeptId)
        {
            return await _conext.Scholarships.Where(x => x.IsDeleted == false && x.DepartmentId == DeptId).ToListAsync();
        }
    }
}
