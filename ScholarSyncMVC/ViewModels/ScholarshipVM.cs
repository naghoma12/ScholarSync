using ScholarSyncMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScholarSyncMVC.ViewModels
{
    public class ScholarshipVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
		public IFormFile PhotoFile { get; set; }
		public string? PhotoURL { get; set; }
		public string? FilePath { get; set; }

		public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Degree { get; set; }
        public Decimal Cost { get; set; }
        public string? Nationality { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string Gender { get; set; }
        public string? HowToApply { get; set; }
        public string? Benefits { get; set; }
        public Category? Category { get; set; }

        public int CategoryId { get; set; }

        public University? University { get; set; }
      
        public int UniversityId { get; set; }
        public Country? Country { get; set; }
        public int CountryId { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }

        public IEnumerable<Country> Countries { get; set; } = new List<Country>();
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<University> Universities { get; set; } = new List<University>();
    }
}
