using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Domain
{
    public class Client
    {
        [Required]
        [Key]
        [Column("cli_id")]
        public int Id { get; set; }
        [Required]
        [MaxLength(11)]
        [Column("cli_cpf")]
        public string? Cpf { get; set; }
        [Column("cli_name")]
        public string? Name { get; set; }

        public ICollection<Orders>? Orders { get; set; }
        public ICollection<ItensOrder>? ItensOrders{ get; set; }

    }
}
