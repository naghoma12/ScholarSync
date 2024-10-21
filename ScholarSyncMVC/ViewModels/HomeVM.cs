using ScholarSyncMVC.Models;

namespace ScholarSyncMVC.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<SimpleDept> Departments { get; set; }= new List<SimpleDept>();
      public IEnumerable<ReviewVM> reviews { get; set; } = new List<ReviewVM>();

        public IEnumerable<Country> countries { get; set; } = new List<Country>();
    }
}
