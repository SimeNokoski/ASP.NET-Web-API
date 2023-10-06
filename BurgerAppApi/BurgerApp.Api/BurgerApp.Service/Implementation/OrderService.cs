using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Domain.Models;
using BurgerApp.DTOs.BurgerDto;
using BurgerApp.DTOs.OrderDto;
using BurgerApp.Mapper;
using BurgerApp.Service.Interfaces;
using BurgerApp.Shared.OrderException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAct.Users;

namespace BurgerApp.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBurgerRepository _burgerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        public OrderService(IOrderRepository orderRepository, IBurgerRepository burgerRepository, IUserRepository userRepository, ILocationRepository locationRepository)
        {
            _orderRepository = orderRepository;
            _burgerRepository = burgerRepository;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
        }

        public void AddBurgerToOrder(BurgerOrderDto burgerOrderDto, int userId)
        {
            var burger = _burgerRepository.GetById(burgerOrderDto.BurgerId);
            var order = _orderRepository.GetById(burgerOrderDto.OrderId);
            var user = _userRepository.GetById(userId);
            if(burger == null)
            {
                throw new OrderException($"burger with {burger.Id} does not exist ");
            }
            if(order == null)
            {
                throw new OrderException($"order with {order.Id} does not exist ");
            }

            var burgerOrder = new BurgerOrder
            {
                Burger = burger,
                Order = order,
                BurgerId = burger.Id,
                OrderId = order.Id
            };
     
            if (order.UserId != user.Id)
            {
                throw new OrderException($"invalid order Id");
            }
            order.BurgerOrders.Add(burgerOrder);
            _orderRepository.Update(order);

        }

        public void CreateOrder(CreateOrderDto createOrderDto, int userId)
        {
            var userDb = _userRepository.GetById(userId);

            var order = createOrderDto.ToCreateOrder();
            var location = _locationRepository.GetById(createOrderDto.LocatioId).ToLocationDto();
            if (!location.OpenNow)
            {
                throw new OrderException($"shop is close");
            }


            order.User = userDb;
            _orderRepository.Add(order);
        }

        public void DeleteOrder(int id, int userId)
        {
            var order = _orderRepository.GetById(id);
            var user = _userRepository.GetById(userId);
            if (order == null)
            {
                throw new OrderException("order with id {id} does not exist");
            }
            if (order.UserId != user.Id)
            {
                throw new OrderException($"invalid order Id");
            }

            _orderRepository.Delete(order);
        }

        public List<OrderDto> GetAllMyOrders(int userId)
        {
            var allOrder = _orderRepository.GetAll();
            return allOrder.Where(x => x.UserId == userId).Select(x => x.ToOrderDto()).ToList();
        }

        public List<OrderDto> GetAllOrders()
        {
            var allOrder = _orderRepository.GetAll();
            if (allOrder == null)
            {
                throw new OrderException("dont have order");
            }
            return allOrder.Select(x => x.ToOrderDto()).ToList();
        }

        public OrderDto GetOrderById(int id)
        {
            var orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new OrderException("order with id {id} does not exist");
            }
            return orderDb.ToOrderDto();
        }

        public void RemoveBurgerInOrder(BurgerOrderDto burgerOrderDto, int userId)
        {
            var user = _userRepository.GetById(userId);
            var order = _orderRepository.GetById(burgerOrderDto.OrderId);
            if (order == null || order.UserId != user.Id)
            {
                throw new OrderException($"invalid order Id");
            }

            var burger = _burgerRepository.GetById(burgerOrderDto.BurgerId);
            if (burger == null)
            {
                throw new OrderException($"burger null");
            }

            BurgerOrder burgerOrder = order.BurgerOrders.Where(x => x.OrderId == burgerOrderDto.OrderId && x.BurgerId == burgerOrderDto.BurgerId).FirstOrDefault();
             
            if (burgerOrder == null)
            {
                throw new OrderException("there is no such burger in this order");
            }

            order.BurgerOrders.Remove(burgerOrder);
            _orderRepository.Update(order);
        }

        public OrderBurgerStatistic Statistic()
        {
            var allOrder = _orderRepository.GetAll();
             var pb = allOrder.SelectMany(x => x.BurgerOrders)
                .GroupBy(x => x.BurgerId)
                .OrderByDescending(x => x.Count())
                .FirstOrDefault();
            var popularBurger = pb.Select(x => x.Burger.Name).FirstOrDefault();
            if (string.IsNullOrEmpty(popularBurger))
            {
                throw new Exception("they currently have no orders");
            }

            var totalOrder = allOrder.Count();

            var location = _locationRepository.GetAll().Select(x => x.Name).ToList();

            var orderIdCount = allOrder.SelectMany(x=>x.BurgerOrders).GroupBy(x=>x.OrderId).Distinct().Count();

            var avrPrice = allOrder.SelectMany(x => x.BurgerOrders)
                .Sum(x => x.Burger.Price) / orderIdCount;


            var statistic = new OrderBurgerStatistic();
            statistic.MostPopularBurger = popularBurger;
            statistic.TotalOrders = totalOrder;
            statistic.Locations = location;
            statistic.AveragePriceOrders = avrPrice;
            return statistic;

        }


        public void UpdateOrder(UpdateOrderDto updateOrderDto, int userId)
        {

           var orderDb = _orderRepository.GetById(updateOrderDto.Id);
            var userDb = _userRepository.GetById(userId);
            var location = _locationRepository.GetById(updateOrderDto.LocatioId);
            if (location == null)
            {
                throw new OrderException($"location with id {updateOrderDto.LocatioId} does not exist");
            }
            if (orderDb == null)
            {
                throw new OrderException($"order with id {updateOrderDto.Id} does not exist");
            }
            if (orderDb.UserId != userDb.Id)
            {
                throw new OrderException($"invalid order Id");
            }
          
            orderDb.UserId = userDb.Id;
            orderDb.User = userDb;
            orderDb.LocationId = updateOrderDto.LocatioId;
            orderDb.IsDelivered = updateOrderDto.IsDelivered;
       
            
            _orderRepository.Update(orderDb);
            
        }
    }
}
