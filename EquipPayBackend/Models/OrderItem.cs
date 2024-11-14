namespace EquipPayBackend.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int? IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
        public int? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; } // Price at the time of ordering
    }
}
