using TicketManagement.DAL;

namespace TicketManagement.BL
{
    public class TicketManager : ITicketManager
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketManager(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public void Add(TicketDto ticketDto)
        {
            Ticket ticket = new Ticket()
            {
                Id = ticketDto.Id,
                Title = ticketDto.Title,
                Description = ticketDto.Description,
                DepartmentId = ticketDto.DepartmentId
            };
            _ticketRepository.Add(ticket);
        }

        public void Delete(int id)
        {
            _ticketRepository.Delete(id);
        }

        public List<TicketDto> GetAll()
        {
            IEnumerable<Ticket> tickets = _ticketRepository.GetAll();
            return tickets.Select(ticket => new TicketDto()
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                DepartmentId = ticket.DepartmentId
            })
            .ToList();

        }

        public TicketDto? GetById(int id)
        {
            Ticket ticket = _ticketRepository.GetById(id);
            return new TicketDto()
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                DepartmentId = ticket.DepartmentId
            };
        }

        public void Update(TicketDto ticketDto)
        {
            Ticket ticket = new Ticket()
            {
                Id = ticketDto.Id,
                Title = ticketDto.Title,
                Description = ticketDto.Description,
                DepartmentId = ticketDto.DepartmentId
            };
            _ticketRepository.Update(ticket);
        }
    }
}
