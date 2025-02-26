using Menssageria.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class OrderDelivery
    {
        public OrderDelivery(int orderDeliveryId, string ?orderCpfId, double orderTotalPrice)
        {
            OrderDeliveryId = orderDeliveryId;
            OrderCpfId = orderCpfId;
            OrderTotalPrice = orderTotalPrice;
        }

        public int OrderDeliveryId { get; set; }
        public string ?OrderCpfId { get; set; }
        public  double OrderTotalPrice { get; set; }
     }
}
