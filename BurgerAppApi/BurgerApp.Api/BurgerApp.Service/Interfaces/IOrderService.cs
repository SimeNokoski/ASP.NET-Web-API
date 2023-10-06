using BurgerApp.Domain.Models;
using BurgerApp.DTOs.BurgerDto;
using BurgerApp.DTOs.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Service.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetAllOrders();
        OrderDto GetOrderById(int id);
        void CreateOrder(CreateOrderDto createOrderDto, int userId);
        void DeleteOrder(int id, int userId);
        void UpdateOrder(UpdateOrderDto updateOrderDto, int userId);
        void AddBurgerToOrder(BurgerOrderDto burgerOrderDto, int userId);
        void RemoveBurgerInOrder(BurgerOrderDto burgerOrderDto, int userId);
        List<OrderDto> GetAllMyOrders(int userId);
        OrderBurgerStatistic Statistic();

    }
}
