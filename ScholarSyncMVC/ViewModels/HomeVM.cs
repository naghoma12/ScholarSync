using ScholarSyncMVC.Models;

namespace ScholarSyncMVC.ViewModels
{
    public class HomeVM
    {
        public List<SimpleDept> Departments { get; set; }= new List<SimpleDept>();
      public IEnumerable<ReviewVM> reviews { get; set; } = new List<ReviewVM>();

        public List<Country> countries { get; set; } = new List<Country>();
    }
}
