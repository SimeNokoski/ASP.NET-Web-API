using BurgerApp.DTOs.OrderDto;
using BurgerApp.Service.Interfaces;
using BurgerApp.Shared.LocationException;
using BurgerApp.Shared.OrderException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BurgerApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IBurgerService _burgerService;
        private readonly IUserService _userService;
        public OrderController(IOrderService orderService, IBurgerService burgerService, IUserService userService)
        {
            _orderService = orderService;
            _burgerService = burgerService;
            _userService = userService;
        }
        
        [HttpGet, Authorize(Roles = "superAdmin")]
        public IActionResult GetAllOrders()
        {
            try
            {         
                return Ok(_orderService.GetAllOrders());
            }
            catch (OrderException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [HttpGet("myorders")]
        public IActionResult GetMyOrders()
        {
            try
            {
                var userId = GetAuthorizedUserId();
                return Ok(_orderService.GetAllMyOrders(userId));
            }
            catch (OrderException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }


        [HttpGet("{id}"), Authorize(Roles = "superAdmin")]
        public IActionResult GetOrderById(int id)
        {
            try
            {     
                return Ok(_orderService.GetOrderById(id));
            }
            catch (OrderException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }
       
        [HttpPost]
        public IActionResult CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _orderService.CreateOrder(createOrderDto,userId);
                return Ok("create order");
            }
            catch (OrderException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }
    
        [HttpPost("AddBurgerInOrder")]
        public IActionResult AddBurgerInOrder(BurgerOrderDto burgerOrderDto)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _orderService.AddBurgerToOrder(burgerOrderDto,userId);
                return Ok("add burger in order");
            }
            catch (OrderException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }
 
        [HttpDelete("removeburgerinorder")]
        public IActionResult RemoveBurgerInOrder(BurgerOrderDto burgerOrderDto)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _orderService.RemoveBurgerInOrder(burgerOrderDto, userId);
                return Ok("remove burger in order");
            }
            catch (OrderException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }


        [HttpDelete("{id}"), Authorize(Roles = "superAdmin")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _orderService.DeleteOrder(id,userId);
                return Ok("delete order");
            }
            catch (OrderException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "System error occurred, contact admin!");
            }
        }

        [HttpPut]
        public IActionResult UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _orderService.UpdateOrder(updateOrderDto,userId);
                return Ok("update order");
            }
            catch (OrderException ex)
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
      
        [HttpGet("statistic")] 
        public IActionResult StatisticOrder()
        {
            try
            {
                var claims = User.Claims;
                string userRole = claims.First(x => x.Type == ClaimTypes.Role).Value;
                if (userRole != "superAdmin")
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                return Ok(_orderService.Statistic());
            }
            catch (OrderException ex)
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
