﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.DTOs.TicketDTO
{
    public class UpdateTicketDto
    {
        public int Id { get; set; }
        public List<int> TicketNumbers { get; set; }


    }
}
