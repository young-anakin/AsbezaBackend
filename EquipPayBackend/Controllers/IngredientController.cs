using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.IngredientDTO;
using EquipPayBackend.DTOs.RoleDTO;
using EquipPayBackend.DTOs.UserDTO;
using EquipPayBackend.Services.IngredientService;
using EquipPayBackend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipPayBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class IngredientController : ControllerBase
    {

        private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
        }
        [HttpPost]
        public async Task<IActionResult> AddIngredient(AddIngredientDTO ingredientDTO)
        {
            try
            {
                return Ok(await _ingredientService.PostIngredient(ingredientDTO));
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteIngredient([FromQuery] IdDTO DTO)
        {
            try
            {
                return Ok(await _ingredientService.DeleteIngredient(DTO));
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetIngredients()
        {
            try
            {
                var roles = await _ingredientService.GetIngredients();
                return Ok(roles);
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });
            }
        }
        [HttpGet("GetSpecificIngredient")]
        public async Task<ActionResult> GetSpecificIngredient([FromQuery] IdDTO DTO)
        {
            try
            {
                var roles = await _ingredientService.GetIngredient(DTO);
                return Ok(roles);
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateRecipe(UpgradeIngredientDTO DTO)
        {
            try
            {
                return Ok(await _ingredientService.UpdateIngredient(DTO));
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });
            }
        }
    }
}
