using System.ComponentModel.DataAnnotations;

namespace UserRoles.ViewModels
{
    /*
     * The LoginViewModel class is a simple data transfer object (DTO) used to capture user input during the login process. 
     * It contains properties for the user's email, password, and a "Remember Me" option, along with validation attributes to ensure that the input meets certain criteria.
     * 
     * The [Required] attribute ensures that the Email and Password fields are not left empty, while the [EmailAddress] attribute validates that the Email field contains a valid email format. 
     * The [DataType(DataType.Password)] attribute specifies that the Password field should be treated as a password input, which typically masks the characters entered by the user.
     */
    public class LoginViewModel
    {
        /*
         * The Email property represents the user's email address. It is decorated with validation attributes to enforce that it is required and must be in a valid email format.
         */
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        /*
         * The Password property represents the user's password. It is decorated with validation attributes to enforce that it is required and should be treated as a password input.
         */
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /*
         * The RememberMe property represents a boolean option for the user to indicate whether they want to stay logged in on the device. 
         * It is decorated with the [Display] attribute to specify a user-friendly label for the field.
         */
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
}
