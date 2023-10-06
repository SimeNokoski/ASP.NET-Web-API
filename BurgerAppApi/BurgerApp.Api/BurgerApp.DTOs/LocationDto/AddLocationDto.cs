﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.DTOs.LocationDto
{
    public class AddLocationDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime OpenAt { get; set; }
        public DateTime CloseAt { get; set; }
    }
}
