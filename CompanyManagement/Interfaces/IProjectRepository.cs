using CompanyManagement.Models;
namespace CompanyManagement.Interfaces
{
    public interface IProjectRepository
    {
        ICollection<Project> GetProjects();
        Project GetProjectById(int id);
        ICollection<Derpartment> GetDepartmentByProjects(int deparmentId);
        bool ProjectExits(int id);
        bool CreateProject(Project project);
        bool DeleteProject(Project project);
        bool Save();
    }
}
