using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScholarSyncMVC.ViewModels
{
    public class PasswordChangeVM
    {
        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(25, ErrorMessage = "Max 25 characters allowed.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(25, ErrorMessage = "Max 25 characters allowed.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Compare("NewPassword", ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
