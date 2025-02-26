
using System.Configuration;
using Dapper;
using Menssageria.Model.Entities;
using Menssageria.Model.Interfaces;
using Model.Interfaces;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;



namespace Mensageria.Repository.DataConnection
{
    public class DataConnection : IDataConnection
    {
        private string _ConnectionString { get; set; }
        private IOrderProductRepository orderProductRepository { get; set; }
        public DataConnection(string connectionString, IOrderProductRepository orderProductRepository)
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            this.orderProductRepository = orderProductRepository;
        }


        public void Conector(Message message)
        {

            using (var conn = new MySqlConnection(_ConnectionString))
            {
                conn.Open();
                var valid = Validator(message, conn);
                if (String.IsNullOrEmpty(valid))
                {
                    string insertOrderDelivery = CreateStringInsertOrderDelivery(message.CodigoPedido, valid, message.TotalPedido);
                    string insertOrderProduct = string.Empty;

                    insertOrderProduct = orderProductRepository.Insert(new Model.Entities.OrderProduct() { op_order_id = message.CodigoPedido, orders = message.Itens });

                    conn.Execute(insertOrderDelivery);
                    conn.Execute(insertOrderProduct);
                    conn.Close();
                }

            }
        }

      

        private static string CreateStringInsertOrderDelivery(int codigoPedido , string cpf, double totalPedido)
        {
            return $"INSERT INTO order_delivery (order_id, order_cpf_id) VALUES ({codigoPedido}, {cpf}, {totalPedido})";
        }

        private string Validator(Message message, MySqlConnection conn)
        {

            //validação simples pois front deve validar antes de enviar para fila;
            var sql = "SELECT * FROM client WHERE client_id = @Id";
            var client = conn.QueryFirstOrDefault<Client>(sql, new { Id = message.CodigoCliente });

            if (client != null)
            {
                return client.Cpf;
            }
            return String.Empty;
        }
    }
}
