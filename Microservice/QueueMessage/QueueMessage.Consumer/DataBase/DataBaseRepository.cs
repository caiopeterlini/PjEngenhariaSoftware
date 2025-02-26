using Dapper;
using MySql.Data.MySqlClient;
using QueueMessage.Consumer.Request;

namespace QueueMessage.Consumer.DataBase
{
    public class DataBaseRepository
    {
        public DataBaseRepository()
        {
        }

        public async Task<int> PostOrderDatabase( RequestOrder? requestOrder)
        {
                var retorno = 0;
            try
            {
                if (requestOrder != null)
                {
                    using var connection = new MySqlConnection("Server=localhost;Database=mydb;User=root;Password=admin;");
                    connection.Open();
                    string queryinsertOrder = $"INSERT INTO order (id,cpf_id,total_price ) VALUES ({requestOrder.OrderId},{requestOrder.ClientId} );";
                    string queryinsertItemOrder = "";

                    foreach (Item item in requestOrder.Itens)
                    {
                        queryinsertItemOrder += $" INSERT INTO item_order (order_id,product_id,quantity ) VALUES ({requestOrder.OrderId},{item.ProductId},{item.Quantity} );";
                    }

                    retorno = await connection.ExecuteAsync(queryinsertOrder);
                    retorno += await connection.ExecuteAsync(queryinsertItemOrder);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retorno;
        }








    }
}
