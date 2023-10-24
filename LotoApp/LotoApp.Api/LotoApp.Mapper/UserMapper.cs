using LotoApp.Domain.Models;
using LotoApp.DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user)
        {

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,

            };
        }
    }
}
