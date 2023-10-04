using MovieApp.Domain.Enums;
using MovieApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetAllMovies(int userId);
        List<MovieDto> FilterMovies(int? year, GenreEnum? genre);
        MovieDto GetMovieById(int id);
        void AddMovie(AddMovieDto addMovieDto, int userId);
        void UpdateMovie(UpdateMovieDto updateMovieDto);
        void DeleteMovie(int id);
    }
}
