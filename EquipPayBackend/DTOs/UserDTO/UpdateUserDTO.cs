namespace EquipPayBackend.DTOs.UserDTO
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string UserGender { get; set; } = string.Empty;
        public string UserName { get; set; } = null!;
        public string RoleName { get; set; }

        public string Image { get; set; }
    }
}
