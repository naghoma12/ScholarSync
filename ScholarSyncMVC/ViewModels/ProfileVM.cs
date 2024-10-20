using ScholarSyncMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel;

namespace ScholarSyncMVC.ViewModels
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class ProfileVM
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed.")]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string LastName { get; set; }


        public DateOnly? BirthDate { get; set; }

        public Country? Nationality { get; set; }
        public int? NationalityId { get; set; }

        public Gender? Gender { get; set; }
        public string? PhoneNumber { get; set; }


        [MaxLength(250, ErrorMessage = "Max 250 characters allowed.")]
        public string? Description { get; set; }

        public IFormFile? PhotoFile { get; set; }
        public string? PhotoURL { get; set; }
        public string? FilePath { get; set; }


        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }

        public ICollection<EducationVM> Educations { get; set; } = new List<EducationVM>();

        public IEnumerable<EduLevel> EduLevels{ get; set; } = new List<EduLevel>();
        public IEnumerable<Country> Nationalities { get; set; } = new List<Country>();
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
    }
}
