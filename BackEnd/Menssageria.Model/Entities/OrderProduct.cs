using Menssageria.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class OrderProduct
    {
        public OrderProduct(int Orderid, List<Order> order)
        {
            Id = Orderid;
            Order = order;
        }

        public int Id { get; set; }
        public  List<Order> Order { get; set; }
     }
}
