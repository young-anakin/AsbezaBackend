using EquipPayBackend.Services.CartService;
using EquipPayBackend.Services.IngredientService;
using EquipPayBackend.Services.OrdersService;
using EquipPayBackend.Services.RecipeService;
using EquipPayBackend.Services.RoleService;
using EquipPayBackend.Services.Tools;
using EquipPayBackend.Services.UserService;

namespace EquipPayBackend
{
        public static class AppServiceRegistration
        {
            public static void AddAppServices(this IServiceCollection services)
            {

                services.AddControllers();
                services.AddScoped<IToolsService, ToolsService>();

                services.AddScoped<IUserService, UserService>();

                services.AddScoped<IRoleService, RoleService>();
                
                services.AddScoped<IIngredientService, IngredientService>();
                services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartService, CartService>();
            }

        }
    }
