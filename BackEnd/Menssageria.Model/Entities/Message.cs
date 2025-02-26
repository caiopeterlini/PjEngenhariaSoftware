using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menssageria.Model.Entities
{
    public class Message
    {

        public int CodigoPedido { get; set; }
        public string? CodigoCliente { get; set; }
        public double TotalPedido { get; set; }
        public required List<Order> Itens { get; set; }
    }
}
