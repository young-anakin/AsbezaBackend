using AutoMapper;
using EquipPayBackend.Context;
using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.IngredientDTO;
using EquipPayBackend.DTOs.RecipeDTO;
using EquipPayBackend.Models;
using EquipPayBackend.Services.Tools;
using Microsoft.EntityFrameworkCore;

namespace EquipPayBackend.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public RecipeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Recipe>> GetRecipes()
        {
            return await _context.Recipes.Include(r => r.Ingredients).ToListAsync();
        }

        public async Task<Recipe> GetRecipe(IdDTO DTO)
        {
            var recipe = await _context.Recipes
                .Where(r => r.Id == DTO.Id)
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync();
            if (recipe == null) throw new KeyNotFoundException("Recipe Not Found");

            return recipe;
        }

        public async Task<Recipe> DeleteRecipe(IdDTO DTO)
        {
            var recipe = await _context.Recipes.FindAsync(DTO.Id);
            if (recipe == null) throw new KeyNotFoundException("Recipe Not Found");
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        //public async Task<Recipe> PostRecipe(AddRecipeDTO DTO)
        //{
        //    var recipe = _mapper.Map<Recipe>(DTO);
        //    _context.Recipes.Add(recipe);
        //    await _context.SaveChangesAsync();
        //    return recipe;
        //}
        public async Task<Recipe> OrderRecipeAsync(AddRecipeDTO DTO)
        {
            if (DTO == null) throw new ArgumentNullException(nameof(DTO));

            var recipe = _mapper.Map<Recipe>(DTO);
            float total = 0;

            if (recipe != null)
            {
                foreach (var ingredientDetail in DTO.IngredientDescription)
                {
                    if (ingredientDetail.Length != 2)
                    {
                        throw new ArgumentException("Each ingredient description must have exactly two elements: ingredientId and quantity.");
                    }
                    int ingredientId = ingredientDetail[0];
                    int quantity = ingredientDetail[1];

                    var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredientId);
                    if (ingredient != null)
                    {
                        recipe.Ingredients.Add(new RecipeIngredient
                        {
                            Recipe = recipe,
                            Ingredient = ingredient,
                            Quantity = quantity
                            
                        });
                        total += quantity * ingredient.Price;
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Ingredient with ID {ingredientId} not found.");
                    }
                }

                try
                {
                    //recipe
                    recipe.Price = total;
                   
                    _context.Recipes.Add(recipe);
                    await _context.SaveChangesAsync();
                    return recipe;
                }
                catch (DbUpdateException ex)
                {
                    // Log the exception or return a detailed error
                    var errorMessage = ex.InnerException?.Message ?? ex.Message;
                    throw new Exception($"An error occurred while saving the entity changes: {errorMessage}");
                }
            }

            throw new Exception("Recipe could not be created");
        }


public async Task<Recipe> UpdateRecipe(UpdateRecipeDTO DTO)
        {
            var recipe = await _context.Recipes
                                .Where(ro => ro.Id == DTO.Id)
                                .FirstOrDefaultAsync();
            if (recipe == null) throw new KeyNotFoundException("Recipe Not Found");
            recipe = _mapper.Map(DTO, recipe);
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

    }
}
