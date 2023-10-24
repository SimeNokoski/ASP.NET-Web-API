using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.DTOs.TicketDTO
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int Number7 { get; set; }
        public string? Prizes { get; set; }

        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public int SessionId { get; set; }

    }
}
