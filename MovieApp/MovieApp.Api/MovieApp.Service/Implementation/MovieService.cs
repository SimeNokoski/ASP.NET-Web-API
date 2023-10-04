using MovieApp.DataAccess.Interfaces;
using MovieApp.Domain.Enums;
using MovieApp.Domain.Models;
using MovieApp.DTOs;
using MovieApp.Service.Interfaces;
using MovieApp.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Shared;

namespace MovieApp.Service.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void AddMovie(AddMovieDto addMovieDto, int userId)
        {
            if (string.IsNullOrEmpty(addMovieDto.Title))
            {
                throw new MovieException("Text must not be empty");
            }
            if (addMovieDto.Year <= 0)
            {
                throw new MovieException("Year must not be negative");
            }
            if (!string.IsNullOrEmpty(addMovieDto.Description) && addMovieDto.Description.Length > 250)
            {
                throw new MovieException("Description can not be longer than 250 characters");
            }

            Movie newMovie = addMovieDto.ToMovie();
            newMovie.UserId = userId;
            _movieRepository.Add(newMovie);

        }

        public void DeleteMovie(int id)
        {
            var movieDb = _movieRepository.GetById(id);
            if (movieDb == null)
            {
                throw new MovieNotFoundException($"Movie with id {id} not found");
            }

            _movieRepository.Delete(movieDb);
        }

        public List<MovieDto> FilterMovies(int? year, GenreEnum? genre)
        {
            if (genre.HasValue)
            {
                var enumValues = Enum.GetValues(typeof(GenreEnum))
                                        .Cast<GenreEnum>()
                                        .ToList();

                if (!enumValues.Contains(genre.Value))
                {
                    throw new MovieException("Invalid genre value");
                }
            }
            return _movieRepository.FilterMovies(year, genre)
                .Select(x => x.ToMovieDto()).ToList();
        }

        public List<MovieDto> GetAllMovies(int userId)
        {
            return _movieRepository.GetAll().Where(x => x.UserId == userId).Select(x => x.ToMovieDto()).ToList();
        }

        public MovieDto GetMovieById(int id)
        {
            var movieDb = _movieRepository.GetById(id);
            if (movieDb == null)
            {
                throw new MovieNotFoundException($"Movie with id {id} not found");
            }
            return movieDb.ToMovieDto();
        }

        public void UpdateMovie(UpdateMovieDto updateMovieDto)
        {
            var movieDb = _movieRepository.GetById(updateMovieDto.Id);
            if (movieDb == null)
            {
                throw new MovieNotFoundException($"Movie with id {updateMovieDto.Id} not found");
            }
            if (string.IsNullOrEmpty(updateMovieDto.Title))
            {
                throw new MovieException("Text must not be empty");
            }
            if (updateMovieDto.Year <= 0)
            {
                throw new MovieException("Year must not be negative");
            }
            if (!string.IsNullOrEmpty(updateMovieDto.Description) && updateMovieDto.Description.Length > 250)
            {
                throw new MovieException("Description can not be longer than 250 characters");
            }

            movieDb.Year = updateMovieDto.Year;
            movieDb.Title = updateMovieDto.Title;
            movieDb.Description = updateMovieDto.Description;
            movieDb.Genre = updateMovieDto.Genre;

            _movieRepository.Update(movieDb);
        }
    }
}
