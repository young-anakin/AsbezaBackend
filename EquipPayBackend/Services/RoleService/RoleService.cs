using AutoMapper;
using EquipPayBackend.Context;
using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.RoleDTO;
using EquipPayBackend.Models;
using EquipPayBackend.Services.Tools;
using Microsoft.EntityFrameworkCore;

///*using*/ LinqToDB;


namespace EquipPayBackend.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public RoleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Role> AddRole(AddRoleDTO roleDTO)
        {
            var role = _mapper.Map<Role>(roleDTO);
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }
        public async Task<Role> DeleteRole(IdDTO DTO)
        {
            var role = await _context.Roles
                    .Where(ro => ro.RoleID == DTO.Id)
                    .FirstOrDefaultAsync();

            if (role == null) throw new KeyNotFoundException("Role Not Found.");

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return role;
        }
        public async Task<List<Role>> GetRoles()
        {
            var role = await _context.Roles
                            .Include(ra => ra.UserAccounts)
                             .ToListAsync();
            return role;
        }

        public async Task<Role> UpdateRole(UpdateRoleDTO roleDTO)
        {
            var role = await _context.Roles
                    .Where(ro => ro.RoleID == roleDTO.RoleId)
                    .FirstOrDefaultAsync();
            if (role == null) throw new KeyNotFoundException("Role Not Found");
            role = _mapper.Map(roleDTO, role);
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;

        }

    }
}
