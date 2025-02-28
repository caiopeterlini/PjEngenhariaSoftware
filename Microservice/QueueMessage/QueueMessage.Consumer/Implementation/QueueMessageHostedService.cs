using QueueMessage.Consumer.Contract;

namespace QueueMessage.Consumer.Implementation
{
    public class QueueMessageHostedService : IHostedService
    {
        private Timer _timer;
        private readonly IMessageConsumer _messageConsumer;

        public QueueMessageHostedService(IMessageConsumer messageConsumer)
        {
            _messageConsumer = messageConsumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _messageConsumer.ConsumeMessages();

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

