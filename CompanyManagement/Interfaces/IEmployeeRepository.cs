﻿using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee>GetEmployees();
        Employee GetEmployeeById(int id);
        ICollection<Derpartment> GetDerpartmentByEmployee(int  employeeId);
        bool EmployeeExits(int id);
    }
}