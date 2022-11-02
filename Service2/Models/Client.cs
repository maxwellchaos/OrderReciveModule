using System.ComponentModel.DataAnnotations;

namespace Service2.Models
{
    public class Client
    {
        public Client()
        {
            Orders = new List<Order>();
        }
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? TelegramId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
