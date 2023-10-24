using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Domain.Models
{
    public class Session : BaseEntity
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool Active { get; set; }
        public  List<Ticket> Tickets { get; set; }

        [ForeignKey(nameof(WinningTicketId))]
        public int WinningTicketId { get; set; }
        public WinningTicket WinningTicket { get; set; }
    }
}
