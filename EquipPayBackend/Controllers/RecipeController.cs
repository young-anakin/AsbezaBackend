using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.IngredientDTO;
using EquipPayBackend.DTOs.RecipeDTO;
using EquipPayBackend.Services.IngredientService;
using EquipPayBackend.Services.RecipeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipPayBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
        }
        [HttpPost]
        public async Task<IActionResult> AddRecipe(AddRecipeDTO recipeDTO)
        {
            try
            {
                return Ok(await _recipeService.OrderRecipeAsync(recipeDTO));
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
        [HttpDelete]
        public async Task<ActionResult> DeleteRecipe([FromQuery] IdDTO DTO)
        {
            try
            {
                return Ok(await _recipeService.DeleteRecipe(DTO));
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
        public async Task<ActionResult> GerRecipes()
        {
            try
            {
                var roles = await _recipeService.GetRecipes();
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
        public async Task<ActionResult> GetSpecificRecipe([FromQuery] IdDTO DTO)
        {
            try
            {
                var roles = await _recipeService.GetRecipe(DTO);
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
        public async Task<ActionResult> UpdateRecipe(UpdateRecipeDTO DTO)
        {
            try
            {
                return Ok(await _recipeService.UpdateRecipe(DTO));
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
