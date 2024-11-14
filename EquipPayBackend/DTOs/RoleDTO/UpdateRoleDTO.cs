namespace EquipPayBackend.DTOs.RoleDTO
{
    public class UpdateRoleDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool CanControlPayment { get; set; } = false;
        public bool CanGenerateReport { get; set; } = false;
        public bool CanMagnageUser { get; set; } = false;
        public bool CanManageSetting { get; set; } = false;
        public bool CanManageUserPrivalage { get; set; } = false;
    }
}
