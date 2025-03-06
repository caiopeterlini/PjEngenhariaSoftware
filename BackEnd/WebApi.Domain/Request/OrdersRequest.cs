using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Request
{
    public class OrdersRequest
    {

        public string? CodigoOrder { get; set; }
        public int ClientId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Itens>? ItensP { get; set; }
    }

    public class Itens
    {
        public Product? Produto { get; set; }
        public int Quantity { get; set; }

    }
}
