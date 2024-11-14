namespace EquipPayBackend.DTOs.IngredientDTO
{
    public class AddIngredientDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        public string Image { get; set; }
        public float Price { get; set; }
    }
}
