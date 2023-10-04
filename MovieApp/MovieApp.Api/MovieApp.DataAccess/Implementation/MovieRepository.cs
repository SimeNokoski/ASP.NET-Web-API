using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Interfaces;
using MovieApp.Domain.Enums;
using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Implementation
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieAppDbContext _dbContext;
        public MovieRepository(MovieAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Movie entity)
        {
            _dbContext.Movies.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Movie entity)
        {
            _dbContext.Movies.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Movie> FilterMovies(int? year, GenreEnum? genre)
        {
            if (genre == null && year == null)
            {
                return _dbContext.Movies.ToList();
            }


            if (year == null)
            {
                List<Movie> moviesDb = _dbContext.Movies.Where(x => x.Genre == (GenreEnum)genre).ToList();
                return moviesDb;
            }
            if (genre == null)
            {
                List<Movie> moviesDb = _dbContext.Movies.Where(x => x.Year == year).ToList();
                return moviesDb;
            }
            List<Movie> movies = _dbContext.Movies.Where(x => x.Year == year
                                                         && x.Genre == (GenreEnum)genre).ToList();
            return movies;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _dbContext.Movies;
        }

        public Movie GetById(int id)
        {
            return _dbContext.Movies.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Movie entity)
        {
           _dbContext.Movies.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
