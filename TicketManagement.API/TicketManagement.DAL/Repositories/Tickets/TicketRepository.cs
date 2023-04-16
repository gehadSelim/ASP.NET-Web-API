using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.DAL
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketContext _context;
        public TicketRepository(TicketContext context)
        {
            _context = context;
        }

        public void Add(Ticket entity)
        {
            _context.Set<Ticket>().Add(entity);
        }

        public void Delete(int id)
        {
            Ticket entity = GetById(id);
            if(entity != null)
            {
                _context.Set<Ticket>().Remove(entity);
            }
            
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _context.Set<Ticket>();
        }

        public Ticket? GetById(int id)
        {
            return _context.Set<Ticket>().Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Ticket entity)
        {
            _context.Set<Ticket>().Update(entity);
        }
    }
}
