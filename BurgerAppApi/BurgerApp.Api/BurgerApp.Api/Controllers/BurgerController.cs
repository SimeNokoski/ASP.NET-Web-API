using BurgerApp.DTOs.BurgerDto;
using BurgerApp.Service.Interfaces;
using BurgerApp.Shared.BurgerException;
using BurgerApp.Shared.LocationException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BurgerApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BurgerController : ControllerBase
    {
        private readonly IBurgerService _burgerService;
        public BurgerController(IBurgerService burgerService)
        {
            _burgerService = burgerService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllBurgers()
        {
            try
            {          
                return Ok(_burgerService.GetAllBurger());
            }
            catch (BurgerException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetBurgerById(int id)
        {
            try
            {
                return Ok(_burgerService.GetBurgerById(id));
            }
            catch (BurgerException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }

        }

        [HttpPost, Authorize(Roles = "superAdmin")]
        public IActionResult CreateNewBurger(CreateBurgerDto createBurgerDto)
        {
            try
            {
                _burgerService.CreateBurger(createBurgerDto);
                return StatusCode(201, "add burger");
            }
            catch (BurgerException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");

            }

        }

        [HttpDelete("{id}"), Authorize(Roles = "superAdmin")]
        public IActionResult DeleteBurger(int id)
        {
            try
            {
                //var claims = User.Claims;
                // string userId = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                //string username = claims.First(x => x.Type == ClaimTypes.Name).Value;
                //string userFullName = claims.First(x => x.Type == "userFullName").Value;
               // string userRole = claims.First(x => x.Type == ClaimTypes.Role).Value;
               // if (userRole != "superAdmin")
                //{
                //    return StatusCode(StatusCodes.Status403Forbidden);
               // }
                _burgerService.DeleteBurger(id);
                return Ok("delete burger");
            }
            catch (BurgerException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }
        
        [HttpPut, Authorize(Roles = "superAdmin")]
        public IActionResult UpdateBurger(UpdateBurgerDto updateBurgerDto)
        {
         
            try
            {
                _burgerService.UpdateBurger(updateBurgerDto);
                return Ok("update burger");
            }
            catch (BurgerException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        
        }


    }
}
