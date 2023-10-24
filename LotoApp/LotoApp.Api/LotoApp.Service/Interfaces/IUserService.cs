using LotoApp.DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Service.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        UserDto GetUserById(int id);
        void UpdateUser(UpdateUserDto user, int userId);
        void DeleteUser(int Id);
        void Register(RegisterUserDto registerUserDto);
        string LoginUser(LoginUserDto loginUserDto);
    }
}
