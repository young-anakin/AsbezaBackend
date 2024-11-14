namespace EquipPayBackend.DTOs.CartDTO
{
    public class AddIngredientToCart
    {
        public int UserId { get; set; }
        public int? IngredientId { get; set; }
        public int Quantity { get; set; }
    }
}
