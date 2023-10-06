using BurgerApp.DTOs.LocationDto;
using BurgerApp.DTOs.OrderDto;
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
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllLocation()
        {
            try
            {
                return Ok(_locationService.GetAllLocation());
            }
            catch (LocationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");

            }
        }

        [HttpPost, Authorize(Roles = "superAdmin")]
        public IActionResult AddNewLocation(AddLocationDto addLocationDto)
        {
            try
            {
             
                _locationService.AddLocation(addLocationDto);
                return Ok("add location");
            }
            catch (LocationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }

        }

        [HttpDelete("{id}"), Authorize(Roles = "superAdmin")]
        public IActionResult DeleteLocation(int id)
        {
            try
            {
             
                _locationService.DeleteLocation(id);
                return Ok("delete location");
            }
            catch (LocationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetLocationById(int id)
        {
            try
            {
                return Ok(_locationService.GetByIdLocation(id));
            }
            catch (LocationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [HttpPut, Authorize(Roles = "superAdmin")]
        public IActionResult UpdateLocation(UpdateLocationDto updateLocationDto)
        {
            try
            {
                var claims = User.Claims;
                string userRole = claims.First(x => x.Type == ClaimTypes.Role).Value;
                if (userRole != "superAdmin")
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                _locationService.UpdateLocation(updateLocationDto);
                return Ok("update location");
            }
            catch (LocationException ex)
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
