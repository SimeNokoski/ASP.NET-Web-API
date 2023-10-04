using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.DTOs;
using MovieApp.Service.Interfaces;
using MovieApp.Shared;

namespace MovieApp.Api.Controllers
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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginUserDto model)
        {
            try
            {
                var user = _userService.Authenticate(model);

                if (user == null)
                {
                    return NotFound("Username or Password is incorrect!");
                }

                return Ok(user);
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto model)
        {
            try
            {
                _userService.Register(model);

                return Ok(new ResponseDto() { success = "Successfully registered user!" });
            }
            catch (UserException ex)
            {
                return BadRequest(new ResponseDto() { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
    }
}
