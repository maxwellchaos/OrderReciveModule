using System.ComponentModel.DataAnnotations;

namespace Service2.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}
