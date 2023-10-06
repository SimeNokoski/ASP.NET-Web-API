using BurgerApp.DataAccess.Interfaces;
using BurgerApp.DTOs.OrderDto;
using BurgerApp.Service.Interfaces;
using BurgerApp.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerApp.DTOs.LocationDto;
using BurgerApp.DTOs.BurgerDto;
using BurgerApp.Shared.BurgerException;
using BurgerApp.Domain.Models;
using BurgerApp.Shared.LocationException;

namespace BurgerApp.Service.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public void AddLocation(AddLocationDto addLocationDto)
        {
            var newLocation = addLocationDto.ToLocation();
            if (string.IsNullOrEmpty(addLocationDto.Address) || string.IsNullOrEmpty(addLocationDto.Name))
            {
                throw new LocationException("address or nam is null or empty");
            }
           
            _locationRepository.Add(newLocation);
        }

        public void DeleteLocation(int id)
        {
      
            var locationDb = _locationRepository.GetById(id);
            if (locationDb == null)
            {
                throw new LocationException($"location with id {id} does not exist");

            }
            _locationRepository.Delete(locationDb);
        }

        public List<LocationDto> GetAllLocation()
        {
            var allLocation = _locationRepository.GetAll();

            return allLocation.Select(x=>x.ToLocationDto()).ToList();
        }

        public LocationDto GetByIdLocation(int id)
        {
           var location = _locationRepository.GetById(id);
            if (location == null)
            {
                throw new LocationException($"location with id {id} does not exist");

            }
            return location.ToLocationDto();
        }

        public void UpdateLocation(UpdateLocationDto updateLocationDto)
        {
           var location = _locationRepository.GetById(updateLocationDto.Id);
            if (location == null)
            {
                throw new LocationException($"location with id {updateLocationDto.Id} does not exist");

            }
            location.Address = updateLocationDto.Address;
            location.Name = updateLocationDto.Name;
            location.OpenAt = updateLocationDto.OpenAt;
            location.CloseAt = updateLocationDto.CloseAt;

            _locationRepository.Update(location);
        }
    }
}
