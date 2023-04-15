using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.DAL
{
    public interface IDepartmentRepository
    {
       IEnumerable<Department> GetWithTicketsandDevelopers();
        
    }
}
