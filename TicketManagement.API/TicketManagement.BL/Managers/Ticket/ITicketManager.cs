using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.BL
{
    public interface ITicketManager
    {
        List<TicketDto> GetAll();
        TicketDto? GetById(int id);
        void Add(TicketDto ticketDto);
        void Update(TicketDto ticketDto);
        void Delete(int id);
        void Save();
    }
}
