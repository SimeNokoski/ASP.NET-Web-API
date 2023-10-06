using BurgerApp.DTOs.BurgerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Service.Interfaces
{
    public interface IBurgerService
    {
        List<BurgerDto> GetAllBurger();
        BurgerDto GetBurgerById(int id);
        void CreateBurger(CreateBurgerDto createBurgerDto);
        void UpdateBurger(UpdateBurgerDto updateBurgerDto);
        void DeleteBurger(int id);
      
    }
}
