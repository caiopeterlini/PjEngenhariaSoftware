using Dapper;
using MySql.Data.MySqlClient;
using QueueMessage.Consumer.Request;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace QueueMessage.Consumer.DataBase
{
    public class DataBaseRepository
    {
        public DataBaseRepository()
        {
        }

        public async Task<bool> PostOrderDatabase(RequestOrder? requestOrder)
        {
                    using var connection = new MySqlConnection("Server=localhost;Database=mydb;User=root;Password=admin;");
            try
            {
                if (requestOrder != null)
                {
                    connection.Open();
                    using var transaction = connection.BeginTransaction();

                    string queryinsertOrder = $"INSERT INTO orders (codigo_ordem,cpf_id,total_price ) VALUES ( '{requestOrder.CodigoOrder}',{requestOrder.ClientId}, {requestOrder.TotalPrice} );";
                    Console.WriteLine($"Consumer Insert {queryinsertOrder} : {DateTime.Now}");
                    await connection.ExecuteAsync(queryinsertOrder, transaction);

                    string queryinsertItemOrder = "";

                    foreach (var item in requestOrder.ItensP)
                    {
                        queryinsertItemOrder += $" INSERT INTO item_order (order_cod,product_id,quantity ) VALUES ('{requestOrder.CodigoOrder}',{item.Produto.Id},{item.Quantity} );";
                    }
                    Console.WriteLine($"Consumer Insert {queryinsertItemOrder} : {DateTime.Now}");
                    var commit = await connection.ExecuteAsync(queryinsertItemOrder, transaction);
                     await transaction.CommitAsync();
                    connection.Close();

                    if (commit != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message} : {DateTime.Now}");
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
                    return false;

        }








    }
}

