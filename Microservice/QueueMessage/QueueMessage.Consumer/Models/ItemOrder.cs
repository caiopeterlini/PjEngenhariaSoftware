using System.ComponentModel.DataAnnotations;

namespace QueueMessage.Consumer.Models
{
    public class ItemOrder
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
