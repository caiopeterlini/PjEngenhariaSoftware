using Menssageria.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IOrderDeliveryService
    {
        public void InsertOrderDelivery(Message message);
    }
}
