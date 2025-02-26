using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Infrastructure.Contracts
{
    public interface IMessageQueueRepository
    {
        public Task<string> OpenChannelPublishQueue(string queueName, string body);

    }
}
