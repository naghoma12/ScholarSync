using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScholarSyncMVC.Models
{
    public class Review:BaseEntity
    {
        public string? Message  { get; set; }
        [Range(1,5)]
        public float? Rating { get; set; }

        public AppUser User { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }

        public Scholarship Scholarship { get; set; }
        [ForeignKey(nameof(Scholarship))]
        public int ScholarshipId { get; set; }

        //public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    }
}
