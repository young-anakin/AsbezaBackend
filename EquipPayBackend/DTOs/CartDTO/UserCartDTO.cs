namespace EquipPayBackend.DTOs.CartDTO
{
    public class UserCartDTO
    {
        public List<CartItemDTO> CartItems { get; set; } = new();
        public float TotalPrice { get; set; }
    }
}
