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
            return new ConnectionFactory
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
                await channel.BasicQosAsync(0, 1, false);
                var QueueOk = await channel.QueueDeclareAsync(queue: this._queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                Console.WriteLine($"Consumer {QueueOk.MessageCount} : {DateTime.Now}");
                var linhas = 0;

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {

                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    try
                    {

                        RequestOrder? requestOrder = JsonSerializer.Deserialize<RequestOrder>(message);
                    Console.WriteLine($"Consumer body {requestOrder} : {DateTime.Now}");
                    var mysql = new DataBaseRepository();
                    var consumo = await mysql.PostOrderDatabase(requestOrder);
                        if (consumo)
                        {
                    await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"An error occurred during database processing: {ex.Message}");
                        await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                        Console.WriteLine("Message requeued");
                    }
                };

                await channel.BasicConsumeAsync(this._queue, autoAck: true, consumer: consumer);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }

}
