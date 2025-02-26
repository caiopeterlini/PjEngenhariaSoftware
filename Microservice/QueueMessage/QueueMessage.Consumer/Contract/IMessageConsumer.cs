using RabbitMQ.Client;

namespace QueueMessage.Consumer.Contract
{
    public interface IMessageConsumer
    {
        void ConsumeMessages();

    }
}
