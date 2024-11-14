using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.CartDTO;
using EquipPayBackend.Models;

namespace EquipPayBackend.Services.CartService
{
    public interface ICartService
    {
        Task<Cart> AddIngredientToCart(AddIngredientToCart DTO);
        Task<Cart> AddRecipeToCart(AddToCartDTO addCartItemDTO);
        Task<int> GetTotalCartItems(IdDTO DTO);
        Task<UserCartDTO> GetUserCart(IdDTO DTO);
        Task RemoveIngredientFromCart(RemoveFromCart DTO);
        Task RemoveRecipeFromCart(RemoveFromCart DTO);
    }
}