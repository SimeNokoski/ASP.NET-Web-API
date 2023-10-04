using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.DataAccess;
using MovieApp.DataAccess.Implementation;
using MovieApp.DataAccess.Interfaces;
using MovieApp.Service.Implementation;
using MovieApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Helper
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services)
        {
            services.AddDbContext<MovieAppDbContext>(option => option.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=MovieWorkShop;Trusted_Connection=True;Encrypt=False"));
        }
        public static void InjectRepository(this IServiceCollection services)
        {
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
        public static void InjectService(this IServiceCollection services)
        {
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
