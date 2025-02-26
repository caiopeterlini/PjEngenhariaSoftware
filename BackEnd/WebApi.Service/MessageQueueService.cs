using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Infrastructure.Contracts;
using WebApi.Infrastructure.Contracts.MessageQueue;

namespace WebApi.Service
{
    public class MessageQueueService : IMessageQueueService
    {
        private IMessageQueueRepository _service;

        public MessageQueueService(IMessageQueueRepository service)
        {
            _service = service;
        }

        public async Task<string> OpenChannelPublishQueue(string queueName, string body)
        {
            try
            {
                return await _service.OpenChannelPublishQueue(queueName, body);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

    }
}
