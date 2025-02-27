
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain;
using WebApi.Infrastructure.Contracts.Service;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItensOrderController : ControllerBase
    {
        private readonly IItensOrderService _itensOrderService;

        public ItensOrderController(IItensOrderService itensOrderService)
        {
            _itensOrderService = itensOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ItensOrder>>> GetAllItensOrders()
        {
            try
            {
                var itensOrders = await _itensOrderService.GetAllItensOrdersAsync();
                return Ok(itensOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItensOrder>> GetItensOrderById(int id)
        {
            try
            {
                var itensOrder = await _itensOrderService.GetItensOrderByIdAsync(id);
                if (itensOrder == null)
                {
                    return NotFound();
                }
                return Ok(itensOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ItensOrder>> InsertItensOrder([FromBody] ItensOrder itensOrder)
        {
            try
            {
                var createdItensOrder = await _itensOrderService.InsertItensOrderAsync(itensOrder);
                return CreatedAtAction(nameof(GetItensOrderById), new { id = createdItensOrder.Id }, createdItensOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItensOrder(int id, [FromBody] ItensOrder itensOrder)
        {
            try
            {
                if (id != itensOrder.Id)
                {
                    return BadRequest();
                }

                await _itensOrderService.UpdateItensOrderAsync(itensOrder);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItensOrder(int id)
        {
            try
            {
                await _itensOrderService.DeleteItensOrderAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
