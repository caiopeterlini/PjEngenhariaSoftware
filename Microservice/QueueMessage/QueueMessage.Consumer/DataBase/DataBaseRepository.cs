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

        public async Task PostOrderDatabase( RequestOrder? requestOrder)
        {
            Console.WriteLine($"PostOrderDatabase : {DateTime.Now}");
            int? latestOrderId = null;
            var retorno = 0;
                    using var connection = new MySqlConnection("Server=localhost;Database=mydb;User=root;Password=admin;");
            try
            {
                if (requestOrder != null)
                {
                    connection.Open();
                    using var transaction = connection.BeginTransaction();



                    var query = $"select id from orders ORDER BY id DESC LIMIT 1;";
                    var command = new MySqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();

                    // Leia o id
                    if (await reader.ReadAsync())
                    {
                        latestOrderId = reader.GetInt32("id") + 1;
                    }

                    reader.Close();
                    string queryinsertOrder = $"INSERT INTO orders (id,cpf_id,total_price ) VALUES ({latestOrderId},{requestOrder.ClientId}, {requestOrder.TotalPrice} );";
                    connection.Execute(queryinsertOrder, transaction);
                    transaction.Commit();

                    string queryinsertItemOrder = "";

                    foreach (var item in requestOrder.ItensP)
                    {
                        queryinsertItemOrder += $" INSERT INTO item_order (order_id,product_id,quantity ) VALUES ({latestOrderId},{item.Produto.Id},{item.Quantity} );";
                    }
                    Console.WriteLine($"Consumer Insert {queryinsertOrder} : {DateTime.Now}");
                    Console.WriteLine($"Consumer Insert {queryinsertItemOrder} : {DateTime.Now}");
                    await Task.Delay(40000);
                     await connection.ExecuteAsync(queryinsertItemOrder, transaction);
                     await transaction.CommitAsync();
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

        }








    }
}
