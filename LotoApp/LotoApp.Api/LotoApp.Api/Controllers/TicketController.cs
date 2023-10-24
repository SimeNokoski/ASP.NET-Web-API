using LotoApp.DTOs.TicketDTO;
using LotoApp.Service.Interfaces;
using LotoApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LotoApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("getAllTickets"), Authorize(Roles = "superAdmin")]
        public IActionResult GetAllTickets()
        {
            try
            {
                return Ok(_ticketService.GetAllTicket());
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [HttpPost("createTicket")]
        public IActionResult CreateNewTicket(CreateTicketDto createTicketDto)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _ticketService.CreateTicket(createTicketDto, userId);
                return StatusCode(201,"CreateTicket");
            }
            catch (TicketDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [HttpGet("getTicketBySession/{sessionId}"), Authorize(Roles = "superAdmin")]
        public IActionResult GetAllTicketsBySession(int sessionId)
        {
            try
            {
                return Ok(_ticketService.GetAllTicketBySesion(sessionId));
            }
            catch (SessionDataException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch(TicketDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }

        }

        [HttpGet("GetTicketById/{id}"), Authorize(Roles = "superAdmin")]
        public IActionResult GetTikcetById(int id)
        {
            try
            {
                return Ok(_ticketService.GetTicketById(id));
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }

        }

        [HttpGet("allMyTickets")]
        public IActionResult AllMyTickets()
        {
            try
            {
                var userId = GetAuthorizedUserId();
                return Ok(_ticketService.GetAllMyTicket(userId));
            }
            catch (TicketNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [HttpGet("allMyTicketsBySession")]
        public IActionResult AllMyTicketsBySession(int sessionId)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                return Ok(_ticketService.GetAllMyTicketBySesion(sessionId, userId));
            }

            catch (SessionDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }

        }

        [HttpPut("updateTicket")]
        public IActionResult UpdateTicekt(UpdateTicketDto updateTicketDto)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _ticketService.UpdateTicket(userId, updateTicketDto);
                return Ok("updated");
            }
            catch (TicketDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (SessionDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [HttpDelete("deleteTicket/{id}")]
        public IActionResult DeleteTicket(int id)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _ticketService.DeleteTicket(userId, id);
                return Ok("Deleted ticket");
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (SessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (SessionDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TicketDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [AllowAnonymous]
        [HttpGet("TicketCheck/{id}")]
        public IActionResult TicketPrize(int id)
        {
            try
            {
                return Ok(_ticketService.TicketCheck(id));
            }
            catch(TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
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
