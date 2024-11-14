using AutoMapper;
using EquipPayBackend.Context;
using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.IngredientDTO;
using EquipPayBackend.DTOs.RoleDTO;
using EquipPayBackend.Models;
using EquipPayBackend.Services.Tools;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EquipPayBackend.Services.IngredientService
{
    public class IngredientService : IIngredientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public IngredientService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Ingredient>> GetIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<Ingredient> GetIngredient(IdDTO DTO)
        {
            var ingredient = await _context.Ingredients.FindAsync(DTO.Id);
            if (ingredient == null) throw new KeyNotFoundException("Ingredient Not Found");

            return ingredient;
        }

        public async Task<Ingredient> DeleteIngredient(IdDTO DTO)
        {
            var ingredient = await _context.Ingredients.FindAsync(DTO.Id);
            if (ingredient == null) throw new KeyNotFoundException("Ingredient Not Found");
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;

        }

        public async Task<Ingredient> PostIngredient(AddIngredientDTO DTO)
        {
            var ingredient = _mapper.Map<Ingredient>(DTO);
            ingredient.Image = DTO.Image;
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        public async Task<Ingredient> UpdateIngredient(UpgradeIngredientDTO DTO)
        {
            var ingredient = await _context.Ingredients
                                .Where(ro => ro.Id == DTO.Id)
                                .FirstOrDefaultAsync();
            if (ingredient == null) throw new KeyNotFoundException("Ingredient Not Found");
            ingredient = _mapper.Map(DTO, ingredient);
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }
    }
}
