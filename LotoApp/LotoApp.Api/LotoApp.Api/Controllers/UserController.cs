﻿using LotoApp.DTOs.UserDTO;
using LotoApp.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LotoApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}"), Authorize(Roles = "superAdmin")]
        public IActionResult GetUsersById(int id)
        {
            try
            {
                var users = _userService.GetUserById(id);
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }

        }

        [HttpGet, Authorize(Roles = "superAdmin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [HttpDelete("{id}"), Authorize(Roles = "superAdmin")]
        public IActionResult UserDelete(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok("userdeleted");
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }
        [HttpPut]
        public IActionResult UpdateUser(UpdateUserDto userDto)
        {
            var userId = GetAuthorizedUserId();
            _userService.UpdateUser(userDto, userId);
            return Ok("user updated");
        }

        [AllowAnonymous] //no token needed (we can not be logged in before registration)
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                _userService.Register(registerUserDto);
                return StatusCode(StatusCodes.Status201Created, "User was created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginUserDto loginDto)
        {
            try
            {
                string token = _userService.LoginUser(loginDto);
                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?
                .Value, out var userId))
            {
                string name = User.FindFirst(ClaimTypes.Name)?.Value;
                throw new Exception($"{name} identifier claim does not exist!");
            }
            return userId;
        }
    }
}
