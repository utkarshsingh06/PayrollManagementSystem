using PayrollManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        int AddEmployee(Employee employeeData);
        int UpdateEmployee(Employee employeeData);
        int RemoveEmployee(int employeeId);
    }
}
