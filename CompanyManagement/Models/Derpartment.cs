namespace CompanyManagement.Models
{
    public class Derpartment
    {
        public int Id { get; set; }
        public string DerpartmentName { get; set; }
        public Center Center { get; set; }
        public ICollection<Project>Projects { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
