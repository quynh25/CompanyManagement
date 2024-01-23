namespace CompanyManagement.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public Derpartment Derpartment { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
