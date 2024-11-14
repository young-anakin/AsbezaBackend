namespace EquipPayBackend.DTOs.RecipeDTO
{
    public class UpdateRecipeDTO
    {
        public int Id {  get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
    }
}
