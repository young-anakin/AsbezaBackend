namespace EquipPayBackend.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public Cart Cart { get; set; }

        public int? IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        public int? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        public int Quantity { get; set; }
    }
}
