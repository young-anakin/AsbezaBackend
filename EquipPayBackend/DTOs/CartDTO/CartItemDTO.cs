namespace EquipPayBackend.DTOs.CartDTO
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
