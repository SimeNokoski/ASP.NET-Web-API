using LotoApp.DTOs.WinningTicketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Service.Interfaces
{
    public interface IWinningTicketService
    {
        List<WinningTicketDto> GetAllWinningTicket();
        WinningTicketDto GetWinningTicketById(int id);
        List<int> GenerateWinningTicket();
        WinningTicketDto GetWinningTicketBySession(int sessionId);
    }
}
