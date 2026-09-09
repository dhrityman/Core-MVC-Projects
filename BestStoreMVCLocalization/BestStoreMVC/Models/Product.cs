using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BestStoreMVC.Models
{
    /// <summary>
    /// Step 6: Create a Product model class with properties for Id, Name, Brand, Categorye, Price, Description, ImageFileName, and CreatedAt.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = "";
        [MaxLength(100, ErrorMessage = "Brand cannot be longer than 100 characters.")]
        public string Brand { get; set; } = "";
        [MaxLength(100, ErrorMessage = "Category cannot be longer than 100 characters.")]
        public string Category { get; set; } = "";

        [Precision(16, 2)]
        public decimal Price { get; set; }
       
        public string Description { get; set; } = "";

        [MaxLength(100, ErrorMessage = "ImageFileName cannot be longer than 100 characters.")]
        public string ImageFileName { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }
}
