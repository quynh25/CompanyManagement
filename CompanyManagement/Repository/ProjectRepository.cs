using CompanyManagement.Data;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private DataContext _context;
        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Derpartment> GetDepartmentByProjects(int deparmentId)
        {
            return _context.Projects.Where(p=>p.Id == deparmentId).Select(p=>p.Derpartment).ToList();
        }

        public Project GetProjectById(int id)
        {
           return _context.Projects.Where(p=>p.Id==id).FirstOrDefault();
        }

        public ICollection<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }

        public bool ProjectExits(int id)
        {
            return _context.Derpartments.Any(p=>p.Id==id);
        }
    }
}
