using LotoApp.Domain.Models;
using LotoApp.DTOs.SessionDTO;
using LotoApp.DTOs.TicketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Mapper
{
    public static class SessionMapper
    {
        public static SessionDto ToSessinoDto(this Session session)
        {
            return new SessionDto
            {
                Start = DateTime.Now,
                Active = session.Active,
                End = session.End,
                WinningTicketId = session.WinningTicketId,
                Id = session.Id,
            };
        }

        public static SessionDataDto ToSessionDataDto(this Session session)
        {
            List<TicketDto> ticketsDto = new List<TicketDto>();
 
            List<Ticket> list = session.Tickets.ToList();
            list.ForEach(ticket =>
            {
                ticketsDto.Add(ticket.ToTicketDto());
            });
            return new SessionDataDto
            {
                Start = session.Start,
                Active = session.Active,
                End = session.End,
                TicketDto = ticketsDto,
                WinningTicket = session.WinningTicket.ToWinningTicketDto(),
            };
        }
    }
}
