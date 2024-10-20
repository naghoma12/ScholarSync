using System.ComponentModel.DataAnnotations.Schema;

namespace ScholarSyncMVC.Models
{
	public class Education : BaseEntity
	{
		[ForeignKey(nameof(EduLevel))]
		public int EduLevelId { get; set; }
		public virtual EduLevel EduLevel { get; set; }
		public string Institution { get; set; }
		public int StartDate { get; set; }
		public int? EndDate{ get; set; }
		public bool? DidGraduate { get; set; }

        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
