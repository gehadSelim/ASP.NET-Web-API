using TicketManagement.DAL;

namespace TicketManagement.BL
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IDepartmentRepository _deptReporitory;
        public DepartmentManager(IDepartmentRepository deptReporitory)
        {
            _deptReporitory = deptReporitory;
        }

        public List<DeptWithTicketsDto> GetDeptwithTicketsAndDevCount()
        {
            IEnumerable<Department> departments = _deptReporitory.GetWithTicketsandDevelopers();
            if (departments.Count() == 0)
            {
                return null;
            }
            return departments.Select(dept => new DeptWithTicketsDto
            {
                Id = dept.Id,
                Name = dept.Name,
                Tickets = dept.Tickets.Select(t => new TicketWithDevCountDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    DeveloperCount = t.Developers.Count()
                }).ToList()
            }).ToList();
        }
    }
}
