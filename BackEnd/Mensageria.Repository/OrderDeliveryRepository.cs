using Menssageria.Model.Entities;
using Menssageria.Model.Interfaces;
using Model.Entities;
using Model.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderDeliveryRepository : IOrderDeliveryRepository
    {

        private IDataConnection _dataConnection;

        public OrderDeliveryRepository(IDataConnection dataConnection)
        {
            _dataConnection = dataConnection;
        }

        public void Insert(OrderDelivery orderDelivery)
        {
            _dataConnection.OpenConection();
            var query = CreateStringInsertOrderDelivery(orderDelivery);
            try
            {
                 _dataConnection.ExecuteQuery(query);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }


        private static string CreateStringInsertOrderDelivery(OrderDelivery orderDelivery)
        {
            return $"INSERT INTO order_delivery (order_id, order_cpf_id) VALUES (" +
                $"{orderDelivery.OrderDeliveryId}," +
                $" {orderDelivery.OrderCpfId}," +
                $" {orderDelivery.OrderTotalPrice})";
        }
       
    }
}
