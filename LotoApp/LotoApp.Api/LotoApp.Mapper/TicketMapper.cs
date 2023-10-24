using LotoApp.Domain.Models;
using LotoApp.DTOs.TicketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Mapper
{
    public static class TicketMapper
    {
        public static Ticket ToTicket(this CreateTicketDto createTicketDto)
        {
            return new Ticket
            {
                Number1 = createTicketDto.TicketNumbers[0],
                Number2 = createTicketDto.TicketNumbers[1],
                Number3 = createTicketDto.TicketNumbers[2],
                Number4 = createTicketDto.TicketNumbers[3],
                Number5 = createTicketDto.TicketNumbers[4],
                Number6 = createTicketDto.TicketNumbers[5],
                Number7 = createTicketDto.TicketNumbers[6],
            };
        }

        public static TicketDto ToTicketDto(this Ticket ticket)
        {
            return new TicketDto
            {
                Number1 = ticket.Number1,
                Number2 = ticket.Number2,
                Number3 = ticket.Number3,
                Number4 = ticket.Number4,
                Number5 = ticket.Number5,
                Number6 = ticket.Number6,
                Number7 = ticket.Number7,
                Prizes = ticket.Prizes,
                UserId = ticket.UserId,
                Id = ticket.Id,
                SessionId = ticket.SessionId,
                UserFullName = $"{ticket.User.FirstName} {ticket.User.LastName}"
            };
        }

        public static Ticket ToTicket(this TicketDto ticketDto)
        {
            return new Ticket
            {
                SessionId = ticketDto.SessionId,
                Number1 = ticketDto.Number1,
                Number2 = ticketDto.Number2,
                Number3 = ticketDto.Number3,
                Number4 = ticketDto.Number4,
                Number5 = ticketDto.Number5,
                Number6 = ticketDto.Number6,
                Number7 = ticketDto.Number7,
                Id = ticketDto.Id,
                UserId = ticketDto.UserId,
                Prizes = ticketDto.Prizes,
            };
        }

        public static AllWinningTicketDto ToAllWinningTicketDto(this Ticket ticket)
        {
            return new AllWinningTicketDto
            {
                Number1 = ticket.Number1,
                Number2 = ticket.Number2,
                Number3 = ticket.Number3,
                Number4 = ticket.Number4,
                Number5 = ticket.Number5,
                Number6 = ticket.Number6,
                Number7 = ticket.Number7,
                SessionId = ticket.SessionId,
                Prizes = ticket.Prizes,
                WinningTicketDto = ticket.Session.WinningTicket.ToWinningTicketDto(),
                UserFullName = $"{ticket.User.FirstName} {ticket.User.LastName}"
            };
        }

    }
}
