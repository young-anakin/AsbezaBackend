using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EquipPayBackend.Models
{
    public class UserAccount
    {
        [Key]
        public int UserAccountId { get; set; }
        public string UserName { get; set; } = null!;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [ForeignKey("UserInfo")]
        public int UserID { get; set; } // Foreign key to Employee

        [System.Text.Json.Serialization.JsonIgnore]
        public UserInfo? UserInfo { get; set; } // Navigation property to UserInfo
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]

        public Cart Cart { get; set; } = new();

        public List<Order> Orders { get; set; }
    }
}
