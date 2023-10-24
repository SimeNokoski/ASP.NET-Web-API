using LotoApp.Service.Interfaces;
using LotoApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotoApp.Api.Controllers
{
    [Authorize(Roles = "superAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class WinningTicketController : ControllerBase
    {
        private readonly IWinningTicketService _winningTicketService;
        public WinningTicketController(IWinningTicketService winningTicketService)
        {
            _winningTicketService = winningTicketService;
        }
        [HttpGet("getAllWinningTickets")]
        public IActionResult GetAllWinningTickets()
        {
            try
            {
                return Ok(_winningTicketService.GetAllWinningTicket());
            }
            catch (WinningTicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [HttpGet("getWinningTicketById/{id}")]
        public IActionResult GetWinningTicketById(int id)
        {
            try
            {
                return Ok(_winningTicketService.GetWinningTicketById(id));
            }
            catch (WinningTicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [HttpGet("getWinningTicketBySession/{sessionId}")]
        public IActionResult GetWinningTicketBySession(int sessionId)
        {
            try
            {
                return Ok(_winningTicketService.GetWinningTicketBySession(sessionId));
            }
            catch (SessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(WinningTicketNotFoundException ex)
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
