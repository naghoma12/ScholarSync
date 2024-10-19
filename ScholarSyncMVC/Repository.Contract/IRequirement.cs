using ScholarSyncMVC.Models;

namespace ScholarSyncMVC.Repository.Contract
{
	public interface IRequirement: IGenericRepository<Requirements>
	{
		Task<IEnumerable<Requirements>> GetRequirementsAsync();
		Task<Requirements?> GetRequiredRequirementsAsync(int id);
		Task<IEnumerable<Requirements>> RequirementsOfScholarship(int id);

    }
}
