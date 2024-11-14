using AutoMapper;
using EquipPayBackend.Context;
using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.CartDTO;
using EquipPayBackend.Models;
using EquipPayBackend.Services.Tools;
using Microsoft.EntityFrameworkCore;

namespace EquipPayBackend.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public CartService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Cart> AddRecipeToCart(AddToCartDTO addCartItemDTO)
        {
            var user = await _context.UserAccounts
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .FirstOrDefaultAsync(u => u.UserAccountId == addCartItemDTO.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var cartItem = new CartItem
            {
                CartId = user.Cart.Id,
                //IngredientId = addCartItemDTO.IngredientId,
                RecipeId = addCartItemDTO.RecipeId,
                Quantity = addCartItemDTO.Quantity
            };

            user.Cart.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return user.Cart;
        }
        public async Task<Cart> AddIngredientToCart(AddIngredientToCart DTO)
        {
            var user = await _context.UserAccounts
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .FirstOrDefaultAsync(u => u.UserAccountId == DTO.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var cartItem = new CartItem
            {
                CartId = user.Cart.Id,
                IngredientId = DTO.IngredientId,
                Quantity = DTO.Quantity
            };

            user.Cart.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return user.Cart;
        }
        public async Task RemoveIngredientFromCart(RemoveFromCart DTO)
        {
            var user = await _context.UserAccounts
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .FirstOrDefaultAsync(u => u.UserAccountId == DTO.userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var cartItem = user.Cart.CartItems.FirstOrDefault(ci => ci.IngredientId == DTO.ingredientId);
            if (cartItem != null)
            {
                user.Cart.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Ingredient not found in cart");
            }
        }

        public async Task RemoveRecipeFromCart(RemoveFromCart DTO)
        {
            var user = await _context.UserAccounts
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .FirstOrDefaultAsync(u => u.UserAccountId == DTO.userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var cartItem = user.Cart.CartItems.FirstOrDefault(ci => ci.RecipeId == DTO.ingredientId);
            if (cartItem != null)
            {
                user.Cart.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Recipe not found in cart");
            }
        }

        public async Task<UserCartDTO> GetUserCart(IdDTO DTO)
        {
            var user = await _context.UserAccounts
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.Ingredient)
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.Recipe)
                .FirstOrDefaultAsync(u => u.UserAccountId == DTO.Id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var userCartDTO = new UserCartDTO();
            foreach (var cartItem in user.Cart.CartItems)
            {
                var itemName = cartItem.Ingredient != null ? cartItem.Ingredient.Name : cartItem.Recipe.Name;
                var itemPrice = cartItem.Ingredient != null ? cartItem.Ingredient.Price : cartItem.Recipe.Price;

                userCartDTO.CartItems.Add(new CartItemDTO
                {
                    CartItemId = cartItem.Id,
                    ItemName = itemName,
                    Quantity = cartItem.Quantity,
                    Price = itemPrice
                });

                userCartDTO.TotalPrice += itemPrice * cartItem.Quantity;
            }


            return userCartDTO;
        }
        public async Task<int> GetTotalCartItems(IdDTO DTO)
        {
            var user = await _context.UserAccounts
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .FirstOrDefaultAsync(u => u.UserAccountId == DTO.Id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            int totalItems = user.Cart.CartItems.Sum(ci => ci.Quantity);
            return totalItems;
        }
    }
}
    

