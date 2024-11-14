namespace EquipPayBackend.DTOs.Order
{
    public class OrderHistoryDTO
    {
        public DateTime OrderDate { get; set; }
        public string Location { get; set; }
        public string ReceiptNumber { get; set; }
        public List<OrderItemDTO> ItemsOrdered { get; set; }
    }
}
