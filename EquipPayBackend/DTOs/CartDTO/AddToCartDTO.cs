namespace EquipPayBackend.DTOs.CartDTO
{
    public class AddToCartDTO
    {
        public int UserId { get; set; }
        public int? RecipeId { get; set; }
        public int Quantity { get; set; }
    }
}
