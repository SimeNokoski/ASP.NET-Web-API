using MovieApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Interfaces
{
    public interface IUserService
    {
        UserDto Authenticate(LoginUserDto model);
        void Register(RegisterUserDto model);
    }
}
