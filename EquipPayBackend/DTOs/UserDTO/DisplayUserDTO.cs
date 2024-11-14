namespace EquipPayBackend.DTOs.UserDTO
{
    public class DisplayUserDTO
    {
        public int userID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public string userGender { get; set; } = string.Empty;

        public Boolean IsCurrentlyActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
