using PayrollManagementSystem.Mainmodule;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repository;
using PayrollManagementSystem.Repository.Interfaces;
using PayrollManagementSystem.Services;

namespace PayrollManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            IEmployeeService employeeService = new EmployeeService(employeeRepository);
            IFinancialRepository financialRepository = new FinancialRepository();
            IFinancialService financialService = new FinancialService(financialRepository);
            IPayrollTRepository payrollTRepository = new PayrollRepository();
            IpayrollServices ipayrollService = new PayrollServices(payrollTRepository);
            ITaxRepository taxRepository = new TaxRepository();
            ITaxServices taxServices = new TaxServices(taxRepository);
            Payrollmanagement p1 = new Payrollmanagement(employeeService, financialService,ipayrollService,taxServices);
            p1.ShowMenu();


            // GETALLEMPLOYEES
            //IEmployeeService employeeService = new EmployeeService();
            //employeeService.GetAllEmployees().ToList().ForEach(x =>
            //{
            //    Console.WriteLine(x);

            //});

            // ADD EMPLOYEES


            ////GET EMPLOYEE BY ID
            //IFinancialRepository financialRepository = new FinancialRepository();
            //Console.Write("Enter Employee ID to search: ");
            //int recordid = int.Parse(Console.ReadLine());
            //FinancialRecord record = financialRepository.GetFinancialRecordById(recordid);
            //if (record != null)
            //{
            //    Console.WriteLine("Employee Details:");
            //    Console.WriteLine($"ID: {record.EmployeeID}");
            //    Console.WriteLine($"First Name: {record.RecordDate}");
            //    Console.WriteLine($"Last Name: {record.Description}");
            //    Console.WriteLine($"Email: {record.Amount}");
            //    Console.WriteLine($"Email: {record.RecordType}");
            //}
            //else
            //{
            //    Console.WriteLine("Employee not found.");
            //}


            ////GET EMPLOYEE BY ID
            //IFinancialRepository financialRepository = new FinancialRepository();
            //Console.Write("Enter emp_id to search Fiancial Record: ");
            //int empiD = int.Parse(Console.ReadLine());
            //List<FinancialRecord> finrecord = financialRepository.GetFinancialRecordsForEmployee(empiD);
            //foreach (FinancialRecord item in finrecord)
            //{
            //    Console.WriteLine(item);
            //}


            //DELETE EMPLOYEE
            //Console.Write("Enter Employee ID to delete: ");
            //int employeeId = int.Parse(Console.ReadLine());
            //IEmployeeService employeeService = new EmployeeService();
            //int deleteemployeestatus = employeeService.RemoveEmployee(employeeId);
            //if (deleteemployeestatus > 0)
            //{
            //    Console.WriteLine(deleteemployeestatus);
            //}
            //else
            //{
            //    Console.WriteLine("Bad");
            //}

            //UPDATE THE EMPLOYEE
            //Console.Write("Enter Employee ID to update: ");
            //int employeeId = int.Parse(Console.ReadLine());
            //IEmployeeRepository employeeService = new EmployeeRepository();
            //Employee employee = employeeService.GetEmployeeById(employeeId);
            //if (employee != null)
            //{
            //    Console.Write("Enter new first name: ");
            //    employee.FirstName = Console.ReadLine();
            //    Console.Write("Enter new last name: ");
            //    employee.LastName = Console.ReadLine();
            //    Console.Write("Enter new email: ");
            //    employee.Email = Console.ReadLine();
            //    int empupdatestatus = employeeService.UpdateEmployee(employee);
            //    if (empupdatestatus > 0)
            //    {
            //        Console.WriteLine(empupdatestatus);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Employee not found.");
            //}


        }
    }
}
