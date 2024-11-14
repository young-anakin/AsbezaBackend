using EquipPayBackend.Context;
//using LinqToDB;
using EquipPayBackend.Services.Tools;
using AutoMapper;
using EquipPayBackend.Models;
using EquipPayBackend.DTOs.UserDTO;
using EquipPayBackend.DTOs.LoginDTO;
using Microsoft.EntityFrameworkCore;
using EquipPayBackend.DTOs;
namespace EquipPayBackend.Services.UserService

{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public UserService(ApplicationDbContext context, IMapper mapper, IToolsService toolsService)
        {
            _context = context;
            _mapper = mapper;
            _toolsService = toolsService;
        }
        public async Task<UserInfo> AddUser(AddUserDTO userDTO)
        {

            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == userDTO.RoleName);

            if (role == null)
            {
                // Handle the case where the role does not exist
                throw new ApplicationException($"Role '{userDTO.RoleName}' not found.");
            }
            var userAccount = new UserAccount
            {
                UserName = userDTO.UserName,
                CreatedAt = DateTime.Now,
                Role = role,
            };

            _toolsService.CreatePasswordHash(userDTO.Password, out byte[] PH, out byte[] PS);
            userAccount.PasswordHash = PH;
            userAccount.PasswordSalt = PS;

            var user = new UserInfo
            {
                UserFullName = userDTO.UserFullName,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                DateOfBirth = userDTO.DateOfBirth,
                UserGender = userDTO.UserGender,
                IsCurrentlyActive = true,
                Image = userDTO.Image,

            };
            userAccount.UserID = user.UserId;
            user.UserAccount = userAccount;
            await _context.UserInfos.AddAsync(user);
            await _context.SaveChangesAsync(); // Save changes to the database

            return user;
        }
        public async Task<List<DisplayUserDTO>> GetAllUsers()
        {
            var user = await _context.UserInfos
                                        .Include(e => e.UserAccount)
                                            .ThenInclude(ua => ua.Role)
                                        .OrderByDescending(e => e.UserFullName)
                                        .ToListAsync();

            if (user != null)
            {
                var recordDTOs = user.Select(r => new DisplayUserDTO
                {
                    userID = r.UserAccountId,
                    userGender = r.UserGender,
                    FullName = r.UserFullName,
                    Phone = r.Phone,
                    IsCurrentlyActive = r.IsCurrentlyActive,
                    DateOfBirth = r.DateOfBirth,
                    Email = r.Email,
                    Image = r.Image,
                    CreatedAt = r.UserAccount.CreatedAt,
                    UserName = r.UserAccount != null ? r.UserAccount.UserName : "JOHN DOE",
                    RoleName = r.UserAccount != null && r.UserAccount.Role != null ? r.UserAccount.Role.RoleName : "CUSTOMER",
                })
                .ToList()
                .OrderByDescending(r => r.UserName)
                .ToList();

                return recordDTOs;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserAccount> DeleteUser(IdDTO DTO)
        {
            var user = await _context.UserAccounts
                                   .FirstOrDefaultAsync(e => e.UserAccountId == DTO.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.UserInfo.IsCurrentlyActive = false;
            user.UserInfo.DateOfTermination = DateTime.Now;
            _context.UserAccounts.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserAccount> GetUserByID(IdDTO DTO)
        {
            var user = await _context.UserAccounts
                            .Include(e => e.UserInfo)
                            .Include(ua => ua.Role)
                            .FirstOrDefaultAsync(e => e.UserAccountId == DTO.Id && e.UserInfo.IsCurrentlyActive == true)
                            ?? throw new KeyNotFoundException("Emp not found");
 
            return user;

        }
        public async Task<UserAccount> UpdateUserAccount(UpdateUserDTO userDTO)
        {
            var user = await _context.UserAccounts
                                    .Where(e => e.UserAccountId == userDTO.Id)
                                    .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Employee Not Found");
            //var employee = _mapper.Map<Employee>(employeeDTO);

            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == userDTO.RoleName);


            if (role == null)
            {
                // Handle the case where the role does not exist
                throw new ApplicationException($"Role '{userDTO.RoleName}' not found.");
            }
            user.UpdatedAt = DateTime.Now;
            user.UserName = userDTO.UserName;
            user.UserInfo.UserFullName = userDTO.UserFullName;
            user.UserInfo.DateOfBirth = userDTO.DateOfBirth;
            user.UserInfo.Email  = userDTO.Email;
            user.UserInfo.Phone = userDTO.Phone;
            user.UserInfo.UserGender = userDTO.UserGender;
            user.UserInfo.Image = userDTO.Image;
            //UserAccount.Password = employeeDTO.Password;
            user.Role = role;




            _context.UserAccounts.Update(user);

            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<string> ChangePassword(ChangePasswordDTO DTO)
        {
            var employee = await _context.UserAccounts
            .Where(e => e.UserAccountId == DTO.User_Id)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("User Not Found");
            var OldPassword = DTO.OldPassword;
            if (!_toolsService.VerifyPasswordHash(OldPassword, employee.PasswordHash, employee.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Old Password Invalid!");
            }
            _toolsService.CreatePasswordHash(DTO.New_Password, out byte[] PH, out byte[] PS);
            employee.PasswordHash = PH;
            employee.PasswordSalt = PS;
            _context.UserAccounts.Update(employee);
            await _context.SaveChangesAsync();
            return $"Password Successfully changed to {DTO.New_Password}";
        }
        public async Task<DisplayUserDTO> Login(LoginDTO login)
        {
            var user = await _context.UserAccounts
                            .Where(u => u.UserName == login.UserName)
                            .Include(u => u.UserInfo)
                            .Include(u => u.Role)
                            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("User Name Not found");

            //var userInfo = await _context.UserInfos
            //    .Where(e => e.UserId == user.UserID)
            //    .Include(u => u.UserAccount)
            //        .ThenInclude(r => r.Role)
            //    .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Employee not found");

            if (!_toolsService.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Invalid password");
            }

            var loginUserDTO = new DisplayUserDTO
            {
                userID = user.UserInfo.UserId,
                Email = user.UserInfo.Email,
                Phone = user.UserInfo.Phone,
                DateOfBirth = user.UserInfo.DateOfBirth,
                userGender = user.UserInfo.UserGender,
                CreatedAt = user.CreatedAt,
                FullName = user.UserInfo.UserFullName,
                IsCurrentlyActive = user.UserInfo.IsCurrentlyActive,
                UserName = user.UserName,
                RoleName = user.Role.RoleName,
                Token = _toolsService.CreateToken(user, user.UserInfo)
            };

            return loginUserDTO;
        }


    }
}
