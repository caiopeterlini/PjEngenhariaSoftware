using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;
using WebApi.Infrastructure.Contracts;

namespace WebApi.Infrastructure.Repository
{
    public class MessageQueueRepository : IMessageQueueRepository
    {
        public MessageQueueRepository()
        {

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

        public async Task<string> OpenChannelPublishQueue(string queueName, string body)
        {
            QueueDeclareOk queueok;
            try
            {
                var connectionFactory = this.CreateConfiguration();
                using var connection = await connectionFactory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                queueok = await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false,
             arguments: null);

               await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: Encoding.UTF8.GetBytes(body));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

            return queueok.QueueName;
        }

    }
}
