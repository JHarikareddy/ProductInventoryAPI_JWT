using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Range(1, 100000, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Range(0, 1000, ErrorMessage = "Stock must be >= 0")]
        public int Stock { get; set; }
    }
}