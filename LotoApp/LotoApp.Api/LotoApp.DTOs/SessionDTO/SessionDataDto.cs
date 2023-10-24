using LotoApp.DTOs.TicketDTO;
using LotoApp.DTOs.WinningTicketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.DTOs.SessionDTO
{
    public class SessionDataDto
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool Active { get; set; }
        public WinningTicketDto WinningTicket { get; set; }
        public List<TicketDto> TicketDto { get; set; }
    }
}
