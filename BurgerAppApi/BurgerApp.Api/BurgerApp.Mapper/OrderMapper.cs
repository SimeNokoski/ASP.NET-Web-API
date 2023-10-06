using BurgerApp.Domain.Models;
using BurgerApp.DTOs.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Mapper
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalPrice = order.BurgerOrders.Select(x => x.Burger.Price).Sum(),
                UserFullName = $"{order.User.FirstName} {order.User.LastName}",
                UserAddress = order.IsDelivered ? order.User.Address : "/",
                IsDelivered = order.IsDelivered,
                Burgers = order.BurgerOrders.Select(x=>x.Burger.Name).ToList(),
                LocationName = order.Location.Name,
                LocationId = order.Location.Id,
        };
        } 
        public static Order ToOrder(this OrderDto orderDto)
        {
            return new Order
            {
                Id = orderDto.Id,        
                IsDelivered=orderDto.IsDelivered,
                BurgerOrders = new List<BurgerOrder>()
            };
        }

        public static Order ToCreateOrder(this CreateOrderDto createOrderDto)
        {
            return new Order
            {
                LocationId = createOrderDto.LocatioId,
                IsDelivered = createOrderDto.IsDelivered,  
            };
        }

 
      
    }
}
