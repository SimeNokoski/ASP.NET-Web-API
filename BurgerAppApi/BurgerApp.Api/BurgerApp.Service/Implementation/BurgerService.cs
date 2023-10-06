using BurgerApp.DataAccess.Interfaces;
using BurgerApp.DTOs.BurgerDto;
using BurgerApp.Service.Interfaces;
using BurgerApp.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerApp.Domain.Models;
using BurgerApp.Shared.BurgerException;
using XAct;

namespace BurgerApp.Service.Implementation
{
    public class BurgerService : IBurgerService
    {
        private readonly IBurgerRepository _burgerRepository;
        public BurgerService(IBurgerRepository burgerRepository)
        {
            _burgerRepository = burgerRepository;
        }
        public void CreateBurger(CreateBurgerDto createBurgerDto)
        {
            var burger = createBurgerDto.ToCreateBurger();
            if(string.IsNullOrEmpty(burger.Name))
            {
                throw new BurgerException("Burger Name is null or empty");
            }
            if(burger.Price <= 0 )
            {
                throw new BurgerException("price is lell then 0");
            }
            _burgerRepository.Add(burger);
        }

        public void DeleteBurger(int id)
        {
     
            var burgerDb = _burgerRepository.GetById(id);
            if(burgerDb == null)
            {
                throw new BurgerException($"burger with id {id} does not exist");

            }
            _burgerRepository.Delete(burgerDb);
        }

        public List<BurgerDto> GetAllBurger()
        {
            var allBurgers = _burgerRepository.GetAll();
            return allBurgers.Select(x=>x.ToBurgerDto()).ToList();
        }

        public BurgerDto GetBurgerById(int id)
        {
            var burgerDb = _burgerRepository.GetById(id);
            if(burgerDb == null)
            {
                throw new BurgerException($"burger with id {id} does not exist");

            }
            return burgerDb.ToBurgerDto();
        }

       

        public void UpdateBurger(UpdateBurgerDto updateBurgerDto)
        {
            var burger = _burgerRepository.GetById(updateBurgerDto.Id);
            if(burger == null)
            {
                throw new BurgerException($"burger with id {updateBurgerDto.Id} does not exist");
            }
            if(string.IsNullOrEmpty(updateBurgerDto.Name) || updateBurgerDto.Price <= 0)
            {
                throw new BurgerException("price is lell then 0 or burgername does not exist");

            }

            burger.IsVegan = updateBurgerDto.IsVegan;
            burger.IsVegetarian = burger.IsVegetarian;
            burger.Price = updateBurgerDto.Price;
            burger.Name = updateBurgerDto.Name;
            
            _burgerRepository.Update(burger);
        }
    }
}
