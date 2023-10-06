using BurgerApp.DTOs.LocationDto;
using BurgerApp.DTOs.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Service.Interfaces
{
    public interface ILocationService
    {
        List<LocationDto> GetAllLocation();
        LocationDto GetByIdLocation(int id);
        void AddLocation (AddLocationDto addLocationDto); 
        void UpdateLocation (UpdateLocationDto updateLocationDto);
        void DeleteLocation (int id);
    }
}
