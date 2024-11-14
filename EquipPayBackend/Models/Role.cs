using System.ComponentModel.DataAnnotations;

namespace EquipPayBackend.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        public string RoleName { get; set; } = null!;

        public bool CanControlPayment { get; set; } = false;
        public bool CanGenerateReport { get; set; } = false;
        public bool CanMagnageUser { get; set; } = false;
        public bool CanManageSetting { get; set; } = false;
        public bool CanManageUserPrivalage { get; set; } = false;

        [System.Text.Json.Serialization.JsonIgnore]
        public List<UserAccount> UserAccounts { get; set; } = new();
    }
}
