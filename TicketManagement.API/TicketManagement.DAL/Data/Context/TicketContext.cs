using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.DAL;

namespace TicketManagement.DAL
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options) { }
        DbSet<Ticket> Tickets => Set<Ticket>();
        DbSet<Developer> Developers => Set<Developer>();
        DbSet<Department> Departments => Set<Department>();

    }
}
