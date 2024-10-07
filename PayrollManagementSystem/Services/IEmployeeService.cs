using PayrollManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Services
{
    public interface IEmployeeService
    {
        void  GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        int AddEmployee(Employee employeeData);
        void UpdateEmployee(Employee employeeData);
        void RemoveEmployee(int employeeId);
    }
}
