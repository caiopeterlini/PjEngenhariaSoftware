using System.ComponentModel.DataAnnotations;

namespace QueueMessage.Consumer.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Version { get; set; }
    }
}
