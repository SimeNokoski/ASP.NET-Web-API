using BurgerApp.Domain.Models;
using BurgerApp.DTOs.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Mapper
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
                Address = user.Address,
                UserName = user.UserName,

            };
        }
    }
}
