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
    public class OrderProductRepository : IOrderProductRepository
    {

        private IDataConnection _dataConnection;

        public OrderProductRepository(IDataConnection dataConnection)
        {
            _dataConnection = dataConnection;
        }

        public void Insert(OrderProduct orderProduct)
        {
            _dataConnection.OpenConection();
            var query = CreateStringInsertOrderProduct(orderProduct);
            try
            {
                 _dataConnection.ExecuteQuery(query);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        private static string CreateStringInsertOrderProduct(OrderProduct orderProduct)
        {
            var stringReturn = " ";
            foreach (Menssageria.Model.Entities.Order item in orderProduct.Order)
            {
                stringReturn += $" INSERT INTO order_product (op_order_id,op_product_id, op_qtde) VALUES ({orderProduct.Id}, {item.IdProduct}, {item.Quantity}); ";
            }

            return stringReturn;
        }

       
    }
}
