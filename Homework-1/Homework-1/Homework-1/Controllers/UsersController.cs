using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(StaticDb.Users);
        }

        [HttpGet("index/{index}")]
        public IActionResult GetUserByIndex([FromRoute] int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index can not be a negative");
                }
                if (index >= StaticDb.Users.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Index {index} does not exist");
                }
                return StatusCode(StatusCodes.Status200OK, StaticDb.Users[index]);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }
    }
}
