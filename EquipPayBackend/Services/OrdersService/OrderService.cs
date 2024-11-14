using AutoMapper;
using EquipPayBackend.Context;
using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.Order;
using EquipPayBackend.DTOs.RecipeDTO;
using EquipPayBackend.Models;
using EquipPayBackend.Services.Tools;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EquipPayBackend.Services.OrdersService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task PlaceOrder(PlaceOrderDTO DTO)
        {
            var user = await _context.UserAccounts
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.Ingredient)
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.Recipe)
                .FirstOrDefaultAsync(u => u.UserAccountId == DTO.CustomerId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Create a new order
            var order = new Order
            {
                UserId = DTO.CustomerId,
                Location = DTO.Location,
                OrderDate = DateTime.UtcNow, // Use UTC time for consistency
                ReceiptNumber = GenerateReceiptNumber() // Generate or assign receipt number
            };

            // Copy cart items to order items
            foreach (var cartItem in user.Cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    Order = order,
                    IngredientId = cartItem.IngredientId,
                    RecipeId = cartItem.RecipeId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Ingredient != null ? cartItem.Ingredient.Price : cartItem.Recipe.Price
                };
                order.OrderItems.Add(orderItem);
            }

            // Add order to DbContext and save changes
            _context.Orders.Add(order);
            user.Cart.CartItems.Clear(); // Clear cart items after placing order
            await _context.SaveChangesAsync();
        }
        public async Task<List<OrderHistoryDTO>> GetOrderHistory(IdDTO DTO)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Ingredient)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Recipe)
                .Where(o => o.UserId == DTO.Id)
                .ToListAsync();

            var orderHistory = new List<OrderHistoryDTO>();

            foreach (var order in orders)
            {
                var orderItems = order.OrderItems.Select(oi =>
                {
                    var itemName = oi.Ingredient != null ? oi.Ingredient.Name : oi.Recipe.Name;
                    return new OrderItemDTO
                    {
                        ItemName = itemName,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    };
                }).ToList();

                var orderDTO = new OrderHistoryDTO
                {
                    OrderDate = order.OrderDate,
                    Location = order.Location,
                    ReceiptNumber = order.ReceiptNumber,
                    ItemsOrdered = orderItems
                };

                orderHistory.Add(orderDTO);
            }

            return orderHistory;
        }
        private string GenerateReceiptNumber()
        {
            // Generate receipt number logic (e.g., using timestamp or random generator)
            return Guid.NewGuid().ToString().Substring(0, 8); // Example: Generate a random GUID and take the first 8 characters
        }
    }
}
