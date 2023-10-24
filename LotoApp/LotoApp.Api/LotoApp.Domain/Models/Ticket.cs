using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace LotoApp.Domain.Models
{
    public class Ticket : BaseEntity
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int Number7 { get; set; }
        public string? Prizes { get; set; }

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(SessionId))]
        public int SessionId { get; set; }
        public virtual Session Session { get; set; }
    }
}
