using LotoApp.Domain.Models;
using LotoApp.DTOs.SessionDTO;
using LotoApp.DTOs.TicketDTO;
using LotoApp.DTOs.WinningTicketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Service.Interfaces
{
    public interface ISessionService
    {
        void CreateSession();
        void EndSession(List<int>nums);
        List<SessionDataDto> GetAllSessions();
        SessionDataDto GetSessionById(int id);
        SessionDto ActiveSession();
        List<AllWinningTicketDto> AllWinningTickets();
        List<AllWinningTicketDto> AllWinningTicketsBySession(int sessionId);
    }
}
