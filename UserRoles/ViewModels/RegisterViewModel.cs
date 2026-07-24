using System.ComponentModel.DataAnnotations;

namespace UserRoles.ViewModels
{
    /*
     * The RegisterViewModel class is a data transfer object (DTO) used to capture user input during the registration process. 
     * It contains properties for the user's name, email, password, and password confirmation, along with validation attributes to ensure that the input meets certain criteria.
     * 
     * The [Required] attribute ensures that the Name, Email, Password, and ConfirmPassword fields are not left empty. 
     * The [EmailAddress] attribute validates that the Email field contains a valid email format. 
     * The [StringLength] attribute enforces a minimum and maximum length for the Password field, while the [Compare] attribute ensures that the Password and ConfirmPassword fields match.
     */
    public class RegisterViewModel
    {
        /*
         * The Name property represents the user's name. It is decorated with the [Required] attribute to enforce that it is not left empty.
         */
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /*
         * The Email property represents the user's email address. It is decorated with validation attributes to enforce that it is required and must be in a valid email format.
         */
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        /*
         The PhoneNumber property represents  user's Phone number.
         */
        [Required(AllowEmptyStrings =true)]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }

        /*
         * The Password property represents the user's password. It is decorated with validation attributes to enforce that it is required, must be between 8 and 40 characters long, and must match the ConfirmPassword field.
         */
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
        public string Password { get; set; }

        /*
         * The ConfirmPassword property represents the user's password confirmation. It is decorated with validation attributes to enforce that it is required and must match the Password field.
         */
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
