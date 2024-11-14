using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.LoginDTO;
using EquipPayBackend.DTOs.Order;
using EquipPayBackend.Services.OrdersService;
using EquipPayBackend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipPayBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("PlaceOrder")]
        public async Task<ActionResult> PlaceOrder(PlaceOrderDTO DTO)
        {
            try
            {
                await _orderService.PlaceOrder(DTO);
                return Ok(new { Message = "Order placed successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet("OrderHistory")]
        public async Task<IActionResult> GetOrderHistory([FromQuery]IdDTO DTO)
        {
            try
            {
                var orderHistory = await _orderService.GetOrderHistory(DTO);
                return Ok(orderHistory);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
