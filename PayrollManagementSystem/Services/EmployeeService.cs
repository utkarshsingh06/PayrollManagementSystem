using PayrollManagementSystem.Exceptions;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repository;
using PayrollManagementSystem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public int AddEmployee(Employee employeeData)
        {
            int insertstatus=0;
            try
            {
                insertstatus = _employeeRepository.AddEmployee(employeeData);
                if (insertstatus > 0)
                {
                    
                    Console.WriteLine(insertstatus);
                }
                else
                {
                    Console.WriteLine("error in insertion");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the employee: {ex.Message}");
            }
            Console.WriteLine();
            return insertstatus;
        }

        public void GetAllEmployees()
        {
            try
            {
                List<Employee> allemployees = _employeeRepository.GetAllEmployees();
                foreach (Employee employee in allemployees)
                {
                    Console.WriteLine("| {0,-5} | {1,-15} | {2,-15} | {3,-20} |", employee.EmployeeID, employee.FirstName, employee.LastName, employee.Email);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving all employees: {ex.Message}");
            }
        }

        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = _employeeRepository.GetEmployeeById(employeeId);
            try
            {
                if (employee == null)
                {
                    throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
                    return null;
                }
                return employee;
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();   
                Console.WriteLine();
                return null;
            }
        }

        public void RemoveEmployee(int employeeId)
        {
            try
            {
                Employee employee = _employeeRepository.GetEmployeeById(employeeId);
                if (employee == null)
                {
                    throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
                }

                int empDeleteStatus = _employeeRepository.RemoveEmployee(employeeId);
                if (empDeleteStatus > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Employee with ID {employeeId} removed successfully.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Could not remove employee with ID {employeeId}.");
                    Console.ResetColor();
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An unexpected error occurred while removing the employee: {ex.Message}");
                Console.ResetColor();
            }

        }

        public void UpdateEmployee(Employee employeeData)
        {
            try
            {
                int updatestatus = _employeeRepository.UpdateEmployee(employeeData);
                if (updatestatus > 0)
                {
                    Console.WriteLine(updatestatus);
                }
                else
                {
                    Console.WriteLine("employee not found in records");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the employee: {ex.Message}");
            }

        }
    }
}
