using EquipPayBackend.Models;
using System.ComponentModel.DataAnnotations;
namespace EquipPayBackend.DTOs.UserDTO
{
    public class AddUserDTO
    {
        public string UserFullName { get; set; } 
        public string Email { get; set; } 
        public string Phone { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public string UserGender { get; set; } 
        public string UserName { get; set; } 
        public string Password { get; set; }
        public string RoleName { get; set; }

        public string Image { get; set; }
    }
}



