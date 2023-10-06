using BurgerApp.DataAccess;
using BurgerApp.DataAccess.Implementation;
using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Service.Implementation;
using BurgerApp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Helper
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services , string conectionStrinng)
        {
            services.AddDbContext<BurgerAppDbContext>(option => option.UseSqlServer(conectionStrinng));
        }

        public static void InjectRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBurgerRepository, BurgerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
        }

        public static void InjectService(this IServiceCollection services)
        {
            services.AddTransient<IBurgerService, BurgerService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILocationService, LocationService>();
        }

    }
}
