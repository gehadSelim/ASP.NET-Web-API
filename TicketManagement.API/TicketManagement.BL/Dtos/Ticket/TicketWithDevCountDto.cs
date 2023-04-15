using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.DAL;

namespace TicketManagement.BL
{
    public class TicketWithDevCountDto
    {
        public int Id { get; init; }
        public string Description { get; init; }
        public int DeveloperCount { get; init; }

    }
}
