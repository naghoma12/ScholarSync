using Microsoft.EntityFrameworkCore;
using ScholarSyncMVC.Data;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository.Contract;

namespace ScholarSyncMVC.Repository
{
	public class RequirementRepository:GenericRepository<Requirements>, IRequirement
	{
		private readonly ScholarSyncConext _conext;

		public RequirementRepository(ScholarSyncConext conext):base(conext)
        {
			_conext = conext;
		}

		public async Task<Requirements?> GetRequiredRequirementsAsync(int id)
		{
			return await _conext.Requirements.Include("Scholarship").Where(x=> x.IsDeleted == false && x.Id == id).FirstOrDefaultAsync();
		}
		
		public async Task<IEnumerable<Requirements>> RequirementsOfScholarship(int id)
		{
			return await _conext.Requirements.Where(x => x.IsDeleted == false && x.ScholarShipId == id).ToListAsync();
		}
		public async Task<IEnumerable<Requirements>> GetRequirementsAsync()
		{
			return await _conext.Requirements.Include("Scholarship").Where(x => x.IsDeleted == false).ToListAsync();
		}
	}
}
