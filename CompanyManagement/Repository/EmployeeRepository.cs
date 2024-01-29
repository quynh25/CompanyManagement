using CompanyManagement.Data;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Repository
{
   
    public class EmployeeRepository : IEmployeeRepository
    {
        private DataContext _dataContext;
        public EmployeeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool EmployeeExits(int id)
        {
           return _dataContext.Employees.Any(e => e.Id == id);
        }

        public ICollection<Derpartment> GetDerpartmentByEmployee(int employeeId)
        {
            return _dataContext.Employees.Where(e=>e.Id == employeeId).Select(e=>e.Derpartment).ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _dataContext.Employees.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Employee> GetEmployees()
        {
            return _dataContext.Employees.ToList();
        }
    }
}
