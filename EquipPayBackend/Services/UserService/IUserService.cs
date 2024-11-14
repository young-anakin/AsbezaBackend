using EquipPayBackend.DTOs;
using EquipPayBackend.DTOs.LoginDTO;
using EquipPayBackend.DTOs.UserDTO;
using EquipPayBackend.Models;

namespace EquipPayBackend.Services.UserService
{
    public interface IUserService
    {
        Task<UserInfo> AddUser(AddUserDTO userDTO);
        Task<string> ChangePassword(ChangePasswordDTO DTO);
        Task<UserAccount> DeleteUser(IdDTO DTO);
        Task<List<DisplayUserDTO>> GetAllUsers();
        Task<UserAccount> GetUserByID(IdDTO DTO);
        Task<DisplayUserDTO> Login(LoginDTO login);
        Task<UserAccount> UpdateUserAccount(UpdateUserDTO userDTO);
    }
}