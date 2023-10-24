using LotoApp.Domain.Models;
using LotoApp.DTOs.TicketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Service.Interfaces
{
    public interface ITicketService
    {
        void CreateTicket(CreateTicketDto createTicketDto, int userId);
        List<TicketDto> GetAllTicket();
        TicketDto GetTicketById(int id);
        List<TicketDto> GetAllTicketBySesion(int sessionId);
        List<TicketDto> GetAllMyTicketBySesion(int sessionId,int userId);
        List<TicketDto> GetAllMyTicket(int userId);

        void WinningTickets(int sessionId);
        void UpdateTicket(int userId, UpdateTicketDto updateTicketDto);
        void DeleteTicket(int userId ,int id);
        string TicketCheck(int id);

    }
}
