using QueueMessage.Consumer.Contract;
using QueueMessage.Consumer.DataBase;
using QueueMessage.Consumer.Request;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace QueueMessage.Consumer.Implementation
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly string _queue = "order";

        private ConnectionFactory ConfigureConnectionFactory()
        {
            return  new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "admin",
                Password = "admin",
            };
        }

        public async void ConsumeMessages()
        {
           
            try
            {   
                var connectionFactory = ConfigureConnectionFactory();
                using var connection = await connectionFactory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                var QueueOk = await channel.QueueDeclareAsync(queue: this._queue, durable: false, exclusive: false, autoDelete: false,arguments: null);

                var linhas = 0;
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    
                    RequestOrder? requestOrder = JsonSerializer.Deserialize<RequestOrder>(message);
                    var mysql = new DataBaseRepository();
                    linhas=await mysql.PostOrderDatabase(requestOrder);
                };

                var teste = await channel.BasicConsumeAsync(this._queue, autoAck: true, consumer: consumer);

                connection.Dispose();
                channel.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
