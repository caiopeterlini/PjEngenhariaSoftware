using Microsoft.EntityFrameworkCore;
using QueueMessage.Consumer.DataBase;
using RabbitMQ.Client;
using System.Text;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.Repository;

namespace WebApi.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MySqlDbContext _mySqlDbContext;

        public OrderRepository(MySqlDbContext mySqlDbContext)
        {
            _mySqlDbContext = mySqlDbContext;
        }

        public async Task<List<Orders>> GetAllOrders()
        {
            try
            {
                return await _mySqlDbContext.orders.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Orders>> GetOrderById(int id)
        {
            try
            {
                var query = await(from order in _mySqlDbContext.orders
                            where order.ClientId == id select order).ToListAsync();
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertOrderByQueue(string queueName, string body)
        {
           
                try
                {
                    var connectionFactory = this.CreateConfiguration();
                    using var connection = await connectionFactory.CreateConnectionAsync();
                    using var channel = await connection.CreateChannelAsync();

                    var queueok = await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false,
                    arguments: null);

                    await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: Encoding.UTF8.GetBytes(body));

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);

                }

        }

      

        public async Task DeleteOrderAsync(int id)
        {
            try
            {
                var orders = await _mySqlDbContext.orders.FindAsync(id);
                if (orders != null)
                {
                    _mySqlDbContext.orders.Remove(orders);
                    await _mySqlDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private ConnectionFactory CreateConfiguration()
        {
            return new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "admin",
                Password = "admin",
            };
        }


    }

}
