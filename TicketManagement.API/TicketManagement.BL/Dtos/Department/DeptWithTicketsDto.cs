﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.BL
{
    public class DeptWithTicketsDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public List<TicketWithDevCountDto> Tickets { get; init; } = new();
    }
}
