namespace EquipPayBackend.DTOs.RecipeDTO
{
    public class AddRecipeDTO
    {


        //public int NumberOfIngredients { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public string TotalPrepTime { get; set; }

        public List<int[]> IngredientDescription { get; set; } = new(); // Each int[] contains [ingredientId, quantity]

    }
}
