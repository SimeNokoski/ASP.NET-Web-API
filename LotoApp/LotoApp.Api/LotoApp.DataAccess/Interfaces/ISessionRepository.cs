using LotoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.DataAccess.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Session ActiveSession();
    }
}
