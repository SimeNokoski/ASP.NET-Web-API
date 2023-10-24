
using LotoApp.Service.Interfaces;
using LotoApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotoApp.Api.Controllers
{
    [Authorize(Roles = "superAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IWinningTicketService _winningTicketService;
        private readonly ITicketService _ticketService;
        public SessionController(ISessionService sessionService, IWinningTicketService winningTicketService, ITicketService ticketService)
        {
            _sessionService = sessionService;
            _winningTicketService = winningTicketService;
            _ticketService = ticketService;
        }

        [HttpPost("startSession")]
        public IActionResult StartSession()
        {
            try
            {
                _sessionService.CreateSession();
                return Ok("Start session");
            }
            catch (WinningTicketNotFoundException ex)
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

        [HttpPut("endSession")]
        public IActionResult EndSession()
        {
            try
            {
                var wt = _winningTicketService.GenerateWinningTicket();
                var session = _sessionService.ActiveSession();
                _sessionService.EndSession(wt);
                _ticketService.WinningTickets(session.Id);
               // _sessionService.CreateSession();
                return Ok("End session");
            }

            catch(SessionDataException ex)
            {
                return NotFound(ex.Message);
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

        [HttpGet("allSession")]
        public IActionResult GetAllSession()
        {
            try
            {
                return Ok(_sessionService.GetAllSessions());
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

        [HttpGet("getSessionById/{id}")]
        public IActionResult GetSessionById(int id)
        {
            try
            {
                return Ok(_sessionService.GetSessionById(id));
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

        [HttpGet("activeSession")]
        public IActionResult ActiveSession()
        {
            try
            {
                return Ok(_sessionService.ActiveSession());
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

        [AllowAnonymous]
        [HttpGet("allWinningTickets")]
        public IActionResult AllWinningTickets()
        {
            try
            {
                return Ok(_sessionService.AllWinningTickets());
            }
            catch(SessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }

        }

        [AllowAnonymous]
        [HttpGet("allWinningTicketsBySession/{sessionId}")]
        public IActionResult AllWinningTicketsBySession(int sessionId)
        {
            try
            {
                return Ok(_sessionService.AllWinningTicketsBySession(sessionId));
            }
            catch (SessionNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }


    }
}
