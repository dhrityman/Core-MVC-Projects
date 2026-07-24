using System.ComponentModel.DataAnnotations;

namespace UserRoles.ViewModels
{
    /*
     * The VerifyEmailViewModel class is a simple data transfer object (DTO) used to capture user input for email verification. 
     * It contains a single property for the user's email, along with validation attributes to ensure that the input meets certain criteria.
     * 
     * The [Required] attribute ensures that the Email field is not left empty, while the [EmailAddress] attribute validates that the Email field contains a valid email format.
     */
    public class VerifyEmailViewModel
    {
        /*
         * The Email property represents the user's email address. It is decorated with validation attributes to enforce that it is required and must be in a valid email format.
         */
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
