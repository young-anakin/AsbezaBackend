namespace EquipPayBackend.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }

        public string Image {  get; set; }

        //public int NumberOfIngredients { get; set; }

        public string TotalPrepTime { get; set; }

        public float Price { get; set; }

        public List<RecipeIngredient> Ingredients { get; set; } = new();
    }
}
