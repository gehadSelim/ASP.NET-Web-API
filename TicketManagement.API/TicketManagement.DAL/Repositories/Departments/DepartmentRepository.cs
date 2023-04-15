using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.DAL
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly TicketContext _context;
        public DepartmentRepository(TicketContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetWithTicketsandDevelopers()
        {
            return _context
                .Set<Department>()
                .Include(d => d.Tickets)
                    .ThenInclude(t => t.Developers);
        }
    }
}
