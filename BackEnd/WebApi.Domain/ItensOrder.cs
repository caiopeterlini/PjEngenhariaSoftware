using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Domain
{
    public class ItensOrder
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public Orders Orders { get; set; }

    }
}
