namespace ScholarSyncMVC.ViewModels
{
    public class ScholarshipDetailsVM
    {
        public string Name { get; set; }
        public string UniversityName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Nationality {  get; set; }
        public int? MinAge { get; set; }
        public string Degree { get; set; }
        public Decimal Cost { get; set; }

        public int? MaxAge { get; set; }
        public string Gender { get; set; }
        public string DepartmentName { get; set; }
        public string CountryName { get; set; }
        public string? Description { get; set; }
        public string? HowToApply { get; set; }
        public string? Benefits { get; set; }
        public string PhotoURL { get; set; }
        public List<string> requirements { get; set; } = new List<string>();
    }
}
