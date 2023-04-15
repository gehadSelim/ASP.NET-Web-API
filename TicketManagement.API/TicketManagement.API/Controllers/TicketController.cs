using Microsoft.AspNetCore.Mvc;
using TicketManagement.BL;

namespace TicketManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketManager _ticketManager;
        public TicketController(ITicketManager ticketManager)
        {
            _ticketManager = ticketManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TicketDto>> Get()
        {
            return _ticketManager.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<TicketDto> Get(int id)
        {
            var ticket = _ticketManager.GetById(id);
            if(ticket == null)
            {
                return NotFound();
            }
            return ticket;
        }

        [HttpPost]
        public ActionResult Post([FromBody] TicketDto value)
        {
            _ticketManager.Add(value);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TicketDto value)
        {
            if(id != value.Id)
            {
                return BadRequest();
            }
            if(_ticketManager.GetById(id) == null)
            {
                return NotFound();
            }
            _ticketManager.Update(value);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _ticketManager.Delete(id);
            return Ok();
        }
    }
}
