using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.IngredientDTO;
using EquipPayBackend.Models;

namespace EquipPayBackend.Services.IngredientService
{
    public interface IIngredientService
    {
        Task<Ingredient> DeleteIngredient(IdDTO DTO);
        Task<Ingredient> GetIngredient(IdDTO DTO);
        Task<List<Ingredient>> GetIngredients();
        Task<Ingredient> PostIngredient(AddIngredientDTO DTO);
        Task<Ingredient> UpdateIngredient(UpgradeIngredientDTO DTO);
    }
}