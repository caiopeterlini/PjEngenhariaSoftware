using System.ComponentModel.DataAnnotations;

namespace QueueMessage.Consumer.Models
{
    public class Client
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(11)]
        public int Cpf { get; set; }
        public int Name { get; set; }

    }
}
