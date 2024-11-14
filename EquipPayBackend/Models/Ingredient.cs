namespace EquipPayBackend.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public string Image { get; set; }
        public float Price { get; set; }
    }
}
