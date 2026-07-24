using System.ComponentModel.DataAnnotations;

namespace UserRoles.ViewModels
{
    public class ChangePasswordViewModel
    {
        /*
         * The Email property represents the user's email address. It is decorated with validation attributes to enforce that it is required and must be in a valid email format.
         */
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        /*
         * The NewPassword property represents the user's new password. It is decorated with validation attributes to enforce that it is required, must be between 8 and 40 characters long, and must match the ConfirmNewPassword field.
         */
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password does not match.")]
        public string NewPassword { get; set; }

        /*
         * The ConfirmNewPassword property represents the user's new password confirmation. It is decorated with validation attributes to enforce that it is required and must match the NewPassword field.
         */
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }

    }
}
