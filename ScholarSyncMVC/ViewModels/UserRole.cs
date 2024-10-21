using Microsoft.AspNetCore.Identity;
using ScholarSyncMVC.Models;

namespace ScholarSyncMVC.ViewModels
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public IEnumerable<AppUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
