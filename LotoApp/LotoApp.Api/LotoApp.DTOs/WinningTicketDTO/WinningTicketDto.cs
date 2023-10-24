using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.DTOs.WinningTicketDTO
{
    public class WinningTicketDto
    {
        public int Id { get; set; }
        public List<int> WinnerTicketNumbers { get; set; }
        public int Number8Bonus { get; set; }
    }
}
