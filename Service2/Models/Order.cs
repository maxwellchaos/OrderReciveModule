using System.ComponentModel.DataAnnotations;

namespace Service2.Models
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>();
        }
        [Required]
        public int Id { get; set; }
        public string? Code { get; set; }
        public Client? Client { get; set; }
        public List<Product> Products { get; set; }
    }
}
