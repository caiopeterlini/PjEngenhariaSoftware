using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.MessageQueue;
using WebApi.Infrastructure.Contracts.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private ILogger<OrderController> _logger;
        private IOrderService _orderService;
        private string _QueueName;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
            _QueueName = "order";
        }


        [HttpPost]
        public async Task<IActionResult> InsertOrderAsync(Orders order)
        {
            var responseQueue = "";
            try
            {
                 await _orderService.InsertOrder(_QueueName, JsonSerializer.Serialize(order).ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in InsertOrder" + ex);
                return BadRequest(ex.Message);
            }
           return Accepted(responseQueue);
        }

        [HttpGet]
        public async Task<ActionResult<List<Orders>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _orderService.DeleteOrderAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
