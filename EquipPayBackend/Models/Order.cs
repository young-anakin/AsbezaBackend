namespace EquipPayBackend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserAccount User { get; set; }
        public string Location { get; set; }
        public DateTime OrderDate { get; set; }
        public string ReceiptNumber { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
