using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.DTOs.SessionDTO
{
    public class SessionDto
    {
        public int Id { get; set; } 
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool Active { get; set; }
        public int WinningTicketId { get; set; }

    }
}
