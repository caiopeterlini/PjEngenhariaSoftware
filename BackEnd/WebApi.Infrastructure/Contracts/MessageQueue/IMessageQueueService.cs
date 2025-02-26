using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Infrastructure.Contracts.MessageQueue
{
    public interface IMessageQueueService
    {
        public Task<string> OpenChannelPublishQueue(string queueName, string body);

    }
}
