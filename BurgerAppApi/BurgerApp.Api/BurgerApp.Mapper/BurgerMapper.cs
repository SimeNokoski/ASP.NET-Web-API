using BurgerApp.Domain.Models;
using BurgerApp.DTOs.BurgerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Mapper
{
    public static class BurgerMapper
    {
        public static BurgerDto ToBurgerDto(this Burger burger)
        {
            return new BurgerDto
            {
                IsVegan = burger.IsVegan,
                IsVegetarian = burger.IsVegetarian,
                Name = burger.Name,
                Price = burger.Price,
                HasFries = burger.HasFries,
            };
        }
        public static Burger ToBurger(this BurgerDto burgerDto)
        {
            return new Burger
            {
                IsVegan = burgerDto.IsVegan,
                IsVegetarian = burgerDto.IsVegetarian,
                Name = burgerDto.Name,
                Price = burgerDto.Price,
                HasFries= burgerDto.HasFries,
            };
        }
        public static Burger ToCreateBurger(this CreateBurgerDto createBurgerDto)
        {
            return new Burger
            {
                IsVegan = createBurgerDto.IsVegan,
                IsVegetarian = createBurgerDto.IsVegetarian,
                Name = createBurgerDto.Name,
                Price = createBurgerDto.Price,
                HasFries = createBurgerDto.HasFries,
            };
        }
      
    }
}
