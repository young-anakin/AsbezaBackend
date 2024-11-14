using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.Order;

namespace EquipPayBackend.Services.OrdersService
{
    public interface IOrderService
    {
        Task<List<OrderHistoryDTO>> GetOrderHistory(IdDTO DTO);
        Task PlaceOrder(PlaceOrderDTO DTO);
    }
}