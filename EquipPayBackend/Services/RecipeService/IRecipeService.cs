using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.RecipeDTO;
using EquipPayBackend.Models;

namespace EquipPayBackend.Services.RecipeService
{
    public interface IRecipeService
    {
        Task<Recipe> DeleteRecipe(IdDTO DTO);
        Task<Recipe> GetRecipe(IdDTO DTO);
        Task<List<Recipe>> GetRecipes();
        //Task OrderRecipeAsync(AddRecipeDTO DTO);
        Task<Recipe> OrderRecipeAsync(AddRecipeDTO DTO);

        //Task<Recipe> PostRecipe(AddRecipeDTO DTO);
        Task<Recipe> UpdateRecipe(UpdateRecipeDTO DTO);
    }
}