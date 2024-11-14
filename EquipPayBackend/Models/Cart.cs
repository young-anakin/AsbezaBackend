namespace EquipPayBackend.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public UserAccount User { get; set; }
        public List<CartItem> CartItems { get; set; } = new();
    }
}
