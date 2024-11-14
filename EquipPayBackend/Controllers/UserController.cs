using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.UserDTO;
using EquipPayBackend.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipPayBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
            private readonly IUserService _userService;
            public UserController(IUserService userService)
            {
             _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }
            [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDTO userDTO)
        {
            try
            {
                return Ok(await _userService.AddUser(userDTO));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                if (ex.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Database error: {ex.InnerException.Message}" });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Database error: An error occurred while saving the entity changes." });
            }
            catch (Exception ex)
            {
                // Return a generic error message for unexpected exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error: " + ex.Message });
            }
        }






        //Get all the users
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userService.GetAllUsers());
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

        //Get a specific employee by ID
        [HttpGet("GetSpecificUser")]
        public async Task<ActionResult> GetEmployee([FromQuery] IdDTO id)
        {
            try
            {
                return Ok(await _userService.GetUserByID(id));
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
        //Update employee information


        //Delete an employee
        [HttpDelete("User")]
        public async Task<ActionResult> DeleteEmployee([FromQuery] IdDTO DTO)
        {
            try
            {
                return Ok(await _userService.DeleteUser(DTO));

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
        public async Task<ActionResult> UpdateEmployee(UpdateUserDTO userDTO)
        {
            try
            {
                return Ok(await _userService.UpdateUserAccount(userDTO));

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

        [HttpPut("Change Password")]
        public async Task<ActionResult> changePassword(ChangePasswordDTO pwDTO)
        {
            try
            {
                return Ok(await _userService.ChangePassword(pwDTO));

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
