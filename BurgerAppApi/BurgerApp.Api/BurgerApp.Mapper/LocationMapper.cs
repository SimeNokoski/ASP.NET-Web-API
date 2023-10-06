using BurgerApp.Domain.Models;
using BurgerApp.DTOs.LocationDto;
using BurgerApp.DTOs.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Mapper
{
    public static class LocationMapper
    {
        public static LocationDto ToLocationDto(this Location location)
        {
            return new LocationDto
            {
                Address = location.Address,
                Name = location.Name,
                Id = location.Id,
                CloseAt = location.CloseAt,
                OpenAt = location.OpenAt,
                OpenNow = IsOpen(location)
            };
        }

        public static Location ToLocation(this LocationDto locationDto)
        {
            return new Location
            {
                Address = locationDto.Address,
                Name = locationDto.Name,
                Id=locationDto.Id,
                CloseAt=locationDto.CloseAt,
                OpenAt=locationDto.OpenAt,
            };
        }

        public static Location ToLocation(this AddLocationDto addLocationDto)
        {
            return new Location
            {
                Address = addLocationDto.Address,
                CloseAt = addLocationDto.CloseAt,
                Name = addLocationDto.Name,
                OpenAt = addLocationDto.OpenAt,
                
            };
        }
        private static bool IsOpen(Location location)
        {
            var open = DateTime.Now;
            bool IsOpen = false;
            if (open > location.OpenAt && open < location.CloseAt)
            {
                IsOpen = true;
            }
            return IsOpen;
        }
    }
}
