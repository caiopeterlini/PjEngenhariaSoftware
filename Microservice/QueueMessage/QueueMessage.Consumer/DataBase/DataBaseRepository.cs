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
                    string queryinsertOrder = $"INSERT INTO orders (id,cpf_id,total_price ) VALUES ({requestOrder.Id},{requestOrder.ClientId}, {requestOrder.TotalPrice} );";
                    string queryinsertItemOrder = "";

                    foreach (var item in requestOrder.ItensP)
                    {
                        queryinsertItemOrder += $" INSERT INTO item_order (order_id,product_id,quantity ) VALUES ({requestOrder.Id},{item.Produto.Id},{item.Quantity} );";
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
