using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Domain
{
    public class Orders
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("codigo_ordem")]
        public int CodigoOrdem { get; set; }
        [Column("cpf_id")]
        public int ClientId { get; set; }
        [Column("total_price")]
        public decimal TotalPrice { get; set; }
        public List<ItensOrder>? Itens { get; set; }
        public Client Client { get; set; }
    }
}
