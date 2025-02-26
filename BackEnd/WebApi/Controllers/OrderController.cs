using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.MessageQueue;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private ILogger<OrderController> _logger;
        private IMessageQueueService _queueService;
        private string _QueueName;

        public OrderController(ILogger<OrderController> logger, IMessageQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
            _QueueName = "order";
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrderAsync(Order order)
        {
            var responseQueue = "";
            try
            {
                responseQueue =  await _queueService.OpenChannelPublishQueue(_QueueName, JsonSerializer.Serialize(order).ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in InsertOrder" + ex);
                return BadRequest(ex.Message);
            }
           return Accepted(responseQueue);
        }
    }
}
