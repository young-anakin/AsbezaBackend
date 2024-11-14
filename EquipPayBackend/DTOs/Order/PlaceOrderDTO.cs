using EquipPayBackend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipPayBackend.DTOs.Order
{
    public class PlaceOrderDTO
    {
        public int CustomerId { get; set; }
        //public List<OrderItem> Items { get; set; }
        public string Location { get; set; }
    }
}
