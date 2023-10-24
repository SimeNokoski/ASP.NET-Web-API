using LotoApp.DataAccess;
using LotoApp.DataAccess.Implementation;
using LotoApp.DataAccess.Interfaces;
using LotoApp.Service.Implementation;
using LotoApp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Helper
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LotoAppDbContext>(option=>option.UseSqlServer(connectionString));
        }

        public static void InjectRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository,UserRepository>();
            services.AddTransient<IWinningTicketRepository,WinningTicketRepository>();
            services.AddTransient<ISessionRepository,SessionRepository>();
            services.AddTransient<ITicketRepository,TicketRepository>();
        }

        public static void InjectService(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IWinningTicketService, WinningTicketService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<ITicketService, TicketService>();
        }
    }
}
