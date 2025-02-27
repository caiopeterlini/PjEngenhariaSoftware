using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Domain
{
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("p_name")]
        public string? Name { get; set; }
        [Column("price")]
        public double Price { get; set; }

        public List<ItensOrder>? Itens { get; set; }

    }
}
