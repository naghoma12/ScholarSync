using System.ComponentModel.DataAnnotations.Schema;

namespace ScholarSyncMVC.Models
{
    public class Scholarship : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
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
        public string FilePath { get; set; }

		public string PhotoURL { get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public University University { get; set; }
        [ForeignKey(nameof(University))]
        public int UniversityId { get; set; }

        public Country Country { get; set; }
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public Department Department { get; set; }
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
    }


}
