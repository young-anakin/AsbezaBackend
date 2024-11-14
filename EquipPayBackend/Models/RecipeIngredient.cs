namespace EquipPayBackend.Models
{
    public class RecipeIngredient
    {
        //public int Id { get; set; }
        public int RecipeId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
    }
}
