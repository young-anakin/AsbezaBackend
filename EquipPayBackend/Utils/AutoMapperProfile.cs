using AutoMapper;
using EquipPayBackend.Context;
using EquipPayBackend.DTOs.IngredientDTO;
using EquipPayBackend.DTOs.RecipeDTO;
using EquipPayBackend.DTOs.RoleDTO;

//using EquipPayBackend.Data;
using EquipPayBackend.Models;
//using LinqToDB;
using System.Diagnostics.Metrics;

namespace EquipPayBackend.Utils
{
    public class AutoMapperProfile : Profile
    {
        private readonly IMapper _mapper;

        public AutoMapperProfile(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
        }
        public AutoMapperProfile()
        {
            // BLOG POST
            //CreateMap<AddEmployeeDTO, Employee>();
            CreateMap<AddRoleDTO, Role>();
            CreateMap<AddIngredientDTO, Ingredient>();
            CreateMap<UpgradeIngredientDTO, Ingredient>();
            CreateMap<UpdateRoleDTO, Role>();
            CreateMap<UpdateRecipeDTO, Recipe>();
            CreateMap<AddRecipeDTO, Recipe>();

        }
    }
}
