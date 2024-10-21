using ScholarSyncMVC.Models;

namespace ScholarSyncMVC.ViewModels
{
    public class EducationVM
    {
       
            public int Id { get; set; } // Required for editing
            public string Institution { get; set; }
            public int StartDate { get; set; }
            public int? EndDate { get; set; }
            public int EduLevelId { get; set; } // Dropdown for selecting degree
        public EduLevel? EduLevel { get; set; }
        public bool DidGraduate { get; set; }
        public IEnumerable<EduLevel> eduLevels{ get; set; } = new List<EduLevel>();
    }
}
