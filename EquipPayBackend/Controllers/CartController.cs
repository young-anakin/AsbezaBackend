using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.CartDTO;
using EquipPayBackend.DTOs.LoginDTO;
using EquipPayBackend.Services.CartService;
using EquipPayBackend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipPayBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }
        [HttpPost("AddRecipeToCart")]
        public async Task<ActionResult> AddToCart([FromBody] AddToCartDTO DTO)
        {
            try
            {
                return Ok(await _cartService.AddRecipeToCart(DTO));
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
        [HttpPost("AddIngredientToCart")]
        public async Task<ActionResult> AddIngredientToCart([FromBody] AddIngredientToCart DTO)
        {
            try
            {
                return Ok(await _cartService.AddIngredientToCart(DTO));
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
        [HttpGet("TotalCartItems")]
        public async Task<IActionResult> GetTotalCartItems([FromQuery] IdDTO DTO)
        {
            try
            {
                var totalItems = await _cartService.GetTotalCartItems(DTO);
                return Ok(new { TotalItems = totalItems });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet("GetUserCart")]
        public async Task<IActionResult> GetUserCart([FromQuery]IdDTO DTO)
        {
            try
            {
                var userCart = await _cartService.GetUserCart(DTO);
                return Ok(userCart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpDelete("RemoveIngredient")]
        public async Task<IActionResult> RemoveIngredientFromCart(RemoveFromCart DTO)
        {
            try
            {
                await _cartService.RemoveIngredientFromCart(DTO);
                return Ok(new { Message = "Ingredient removed from cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("RemoveRecipe")]
        public async Task<IActionResult> RemoveRecipeFromCart(RemoveFromCart DTO)
        {
            try
            {
                await _cartService.RemoveRecipeFromCart(DTO);
                return Ok(new { Message = "Recipe removed from cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
