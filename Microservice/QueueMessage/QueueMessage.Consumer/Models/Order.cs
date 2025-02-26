using System.ComponentModel.DataAnnotations;

namespace QueueMessage.Consumer.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CpfId { get; set; }
        public double TotalPrice { get; set; }
    }
}
