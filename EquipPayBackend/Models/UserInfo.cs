using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipPayBackend.Models
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [RegularExpression(@"([0-9]{9}$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string UserGender { get; set; } = string.Empty;
        public bool IsCurrentlyActive { get; set; } = true;
        public DateTime? DateOfTermination { get; set; } = null;

        [ForeignKey("UserAccount")]
        public int UserAccountId { get; set; }

        public UserAccount? UserAccount { get; set; }

        public string Image { get; set; }
    }
}
