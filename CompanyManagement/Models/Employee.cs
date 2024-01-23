namespace CompanyManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Address {  get; set; }
        public string Number {  get; set; }
        public double Salary {  get; set; }
        public Derpartment Derpartment { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set;}
    }
}
