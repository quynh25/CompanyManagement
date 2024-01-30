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

        public bool CreateProject(Project project)
        {
            _context.Add(project);
            return Save();
        }

        public bool DeleteProject(Project project)
        {
            _context.Remove(project);
            return Save();
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
            return _context.Projects.Any(p=>p.Id==id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >0 ?true : false;
        }
    }
}
