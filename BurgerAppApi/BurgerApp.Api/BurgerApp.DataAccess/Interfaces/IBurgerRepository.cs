using BurgerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.DataAccess.Interfaces
{
    public interface IBurgerRepository : IRepository<Burger>
    {
    }
}
