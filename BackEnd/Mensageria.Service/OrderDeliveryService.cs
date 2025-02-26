using Menssageria.Model.Entities;
using Microsoft.Extensions.DependencyInjection;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderDeliveryService : IOrderDeliveryService
    {
        private IOrderDeliveryRepository _orderdeliveryRepository;
        private IOrderProductRepository _orderProductRepository;

        public OrderDeliveryService(ServiceProvider serviceProvider)
        {
            this._orderdeliveryRepository = serviceProvider.GetService<IOrderDeliveryRepository>(); 
            this._orderProductRepository = serviceProvider.GetService<IOrderProductRepository>(); 
        }

        public void InsertOrderDelivery(Message message)
        {
            var orderDelivery = new Model.Entities.OrderDelivery(message.CodigoPedido, message.CodigoCliente, message.TotalPedido);
            var orderProduct = new Model.Entities.OrderProduct(message.CodigoPedido,message.Itens);

            this._orderdeliveryRepository.Insert(orderDelivery);
            this._orderProductRepository.Insert(orderProduct);
        }
    }
}
