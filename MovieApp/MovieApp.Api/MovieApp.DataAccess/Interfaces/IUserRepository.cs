using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
