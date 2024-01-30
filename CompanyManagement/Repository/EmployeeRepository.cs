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

        public bool CreateEmployee(Employee employee)
        {
            _dataContext.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _dataContext.Remove(employee);
            return Save();
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

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true:false;
        }
    }
}
