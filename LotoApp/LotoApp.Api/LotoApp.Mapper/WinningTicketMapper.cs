using LotoApp.Domain.Models;
using LotoApp.DTOs.WinningTicketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Mapper
{
    public static class WinningTicketMapper
    {
        public static WinningTicketDto ToWinningTicketDto(this WinningTicket winningTicket)
        {
            List<int> list = new List<int>();
            list.Add(winningTicket.Number1);
            list.Add(winningTicket.Number2);
            list.Add(winningTicket.Number3);
            list.Add(winningTicket.Number4);
            list.Add(winningTicket.Number5);
            list.Add(winningTicket.Number6);
            list.Add(winningTicket.Number7);

            return new WinningTicketDto
            {
                Id = winningTicket.Id,
                WinnerTicketNumbers = list,
                Number8Bonus = winningTicket.Number8
            };
        }

        public static WinningTicket ToWinningTicket(this WinningTicketDto winningTicketDto)
        {
            return new WinningTicket
            {
                Id = winningTicketDto.Id,
                Number1 = winningTicketDto.WinnerTicketNumbers[0],
                Number2 = winningTicketDto.WinnerTicketNumbers[1],
                Number3 = winningTicketDto.WinnerTicketNumbers[2],
                Number4 = winningTicketDto.WinnerTicketNumbers[3],
                Number5 = winningTicketDto.WinnerTicketNumbers[4],
                Number6 = winningTicketDto.WinnerTicketNumbers[5],
                Number7 = winningTicketDto.WinnerTicketNumbers[6],
                Number8 = winningTicketDto.Number8Bonus,
            };
        }
    }
}
