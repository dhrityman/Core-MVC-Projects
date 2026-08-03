using BestStoreMVC.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace BestStoreMVC.Models
{
    /// <summary>
    ///  STEP 17: Now we are going to create a new Product, so create a new product, create a new model class of Type DTO (Data Transfer Object) named as "ProductDTO" in the folder named "Models" in the project and add properties for each field that we want to include in the form. 
    ///  Which allow to user to submit the product details to the server.This class will be used to transfer data between the view and the controller.
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// Add for the Edit Product, we need to add the Id property to the ProductDTO class so that we can identify which product is being edited.
        /// </summary>
        //public int Id { get; set; }

        [Required(ErrorMessage = "Name is required"), MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Brand is required"), MaxLength(100, ErrorMessage = "Brand cannot be longer than 100 characters.")]
        public string Brand { get; set; } = "";
        [Required(ErrorMessage = "Category is required"), MaxLength(100, ErrorMessage = "Categorye cannot be longer than 100 characters.")]
        public string Category { get; set; } = "";

        [Required(ErrorMessage = "Price is required")]
        //[Range(0.00, double.MinValue, ErrorMessage = "Value must be at least 0.00")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = "";

        /// <summary>
        /// For the ImageFile property, we are using the IFormFile interface, which is provided by ASP.NET Core to represent a file that is uploaded by the user.
        /// This property is Optional because a user may choose not to upload an image for the product. If the user does not upload an image, the ImageFile property will be null.
        /// This Property is only required for Create the product.
        /// </summary>

        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Your image's filetype is not valid.")]
        //[FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Your image's filetype is not valid.")]
        public IFormFile? ImageFile { get; set; }
        
    }
}
