using PayrollManagementSystem.Models;
using PayrollManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Mainmodule
{
    internal class Payrollmanagement
    {
        private readonly IEmployeeService _employeeService;
        private readonly IFinancialService _financialService;
        private readonly IpayrollServices _payrollServices;
        private readonly ITaxServices _taxServices;
        public Payrollmanagement(IEmployeeService employeeService,IFinancialService financialService,IpayrollServices payrollServices,ITaxServices taxServices)
        {
            _employeeService = employeeService;
            _financialService = financialService;
            _payrollServices = payrollServices;
            _taxServices = taxServices;
            
        }
        public void ShowMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("-------------Payroll management System----------");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Employee Management");
                Console.WriteLine("2. Payroll Processing");
                Console.WriteLine("3. Financial Reporting");
                Console.WriteLine("4. Tax Calculation");
                Console.WriteLine("5. Exit");
                Console.ResetColor();
                Console.Write("Choose an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ShowEmployeeMenu();
                        break;
                    case "2":
                        ProcessPayroll();
                        break;
                    case "3":
                        GenerateFinancialReport();
                        break;
                    case "4":
                        TaxCalculation();
                        break;
                    case "5":
                        Console.WriteLine("Exiting");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ShowEmployeeMenu()
        {
            bool continuemenu = true;
            while (continuemenu)
            {
                Console.WriteLine("1. Display all Employee");
                Console.WriteLine("2. Get Employee by ID");
                Console.WriteLine("3. Add Employee");
                Console.WriteLine("4. Remove Employee");
                Console.WriteLine("5. Update Employee");
                Console.WriteLine("6. Back to main menu");
                var option = Console.ReadLine();
                if (option == "1")
                {
                    Console.ForegroundColor= ConsoleColor.Blue;
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("| {0,-5} | {1,-15} | {2,-15} | {3,-20} |", "ID", "First Name", "Last Name", "Email");
                    Console.WriteLine("----------------------------------------------------------------------");
                    _employeeService.GetAllEmployees();
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else if (option == "2")
                {
                    Console.Write("Enter Employee ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Employee e1 = _employeeService.GetEmployeeById(id);
                    if (e1 != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Employee Details:");
                        Console.WriteLine($"ID: {e1.EmployeeID}");
                        Console.WriteLine($"First Name: {e1.FirstName}");
                        Console.WriteLine($"Last Name: {e1.LastName}");
                        Console.WriteLine($"Email: {e1.Email}");
                        Console.ResetColor();
                    }

                }
                else if (option == "3")
                {

                    Console.WriteLine("Enter First Name:");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Enter Last Name:");
                    string lastName = Console.ReadLine();
                    Console.WriteLine("Enter Date of Birth (YYYY-MM-DD):");
                    DateTime dob = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Gender (M/F):");
                    string gender = Console.ReadLine();
                    Console.WriteLine("Enter Email:");
                    string email = Console.ReadLine();
                    Console.WriteLine("Enter Phone Number:");
                    string phone = Console.ReadLine();
                    Console.WriteLine("Enter Address:");
                    string address = Console.ReadLine();
                    Console.WriteLine("Enter Job Title:");
                    string jobTitle = Console.ReadLine();
                    Console.WriteLine("Enter Joining Date (YYYY-MM-DD):");
                    DateTime joiningDate = DateTime.Parse(Console.ReadLine());
                    DateTime? terminationDate = null;
                    Employee newemployee = new Employee
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        DateOfBirth = dob,
                        Gender = gender,
                        Email = email,
                        PhoneNumber = phone,
                        Address = address,
                        Position = jobTitle,
                        JoiningDate = joiningDate,
                        TerminationDate = terminationDate,
                    };
                    _employeeService.AddEmployee(newemployee);
                    Console.WriteLine();
                }
                else if (option == "4")
                {
                    Console.WriteLine("Enter Employee ID: ");
                    int id = int.Parse(Console.ReadLine());
                    _employeeService.RemoveEmployee(id);
                    Console.WriteLine();
                }
                else if (option == "5")
                {
                    Console.WriteLine("Enter Employee ID: ");
                    int id = int.Parse(Console.ReadLine());
                    var employee = _employeeService.GetEmployeeById(id);
                    Console.WriteLine($"Current Employee Details: {employee.FirstName} {employee.LastName} - {employee.Email}");
                    Console.Write("Enter new First Name: ");
                    string newFirstName = Console.ReadLine();
                    Console.Write("Enter new Last Name:  ");
                    string newLastName = Console.ReadLine();
                    Console.Write("Enter new Email:  ");
                    string newEmail = Console.ReadLine();
                    employee.FirstName = newFirstName;
                    employee.LastName = newLastName;
                    employee.Email = newEmail;
                    _employeeService.UpdateEmployee(employee);
                    Console.WriteLine("Employee details updated successfully.");
                }
                else if (option == "6")
                {
                    continuemenu = false;
                }
            }
        }

        private void ProcessPayroll()
        {
            bool backToMainMenu = false;

            while (!backToMainMenu)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Generate a Payroll");
                Console.WriteLine("2. Get a Payroll by Id");
                Console.WriteLine("3. Get a Payroll for Employee");
                Console.WriteLine("4. Get a Payroll for Period");
                Console.WriteLine("5. Back to Main Menu");
                Console.ResetColor();
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        generatepayroll();
                        break;
                    case "2":
                        getpayrollbyid();
                        break;
                    case "3":
                        getpayrollforemployee();
                        break;
                    case "4":
                        getpayrollforperiod();
                        break;
                    case "5":
                        backToMainMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        void generatepayroll()
        {
            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());
            Console.Write("Enter payperiod_start: ");
            DateTime startdate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter payperiod_end: ");
            DateTime enddate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter basic salary: ");
            decimal basicsalary = decimal.Parse(Console.ReadLine());
            Console.Write("Enter overtimepay: ");
            decimal overtimepay = decimal.Parse(Console.ReadLine());
            Console.Write("Enter deductions: ");
            decimal deductions = decimal.Parse(Console.ReadLine());

            Payroll payroll = _payrollServices.GeneratePayroll(employeeId, startdate, enddate, basicsalary, overtimepay, deductions);
            if (payroll != null)
            {
                Console.WriteLine("Payroll Generated Details:");
                Console.WriteLine(payroll.ToString());



            }
            else
            {
                Console.WriteLine("No financial record found with the given ID.");
            }
        }

        void getpayrollbyid()
        {
            Console.Write("Enter payroll ID: ");
            int pid = int.Parse(Console.ReadLine());
            Payroll payroll = _payrollServices.GetPayrollById(pid);
            if (payroll != null)
            {
                Console.WriteLine("Payroll Generated Details:");
                Console.WriteLine(payroll.ToString());
            }
            else
            {
                Console.WriteLine("No financial record found with the given ID.");
            }
        }

        void getpayrollforemployee()
        {
            Console.Write("Enter the EmployeeId ");
            int empid = int.Parse(Console.ReadLine());
            List<Payroll> payroll = _payrollServices.GetPayrollsForEmployee(empid);
            foreach (Payroll item in payroll)
            {
                Console.WriteLine(item);
            }
        }

        void getpayrollforperiod()
        {
            Console.Write("Enter the startdate: ");
            DateTime startdate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter the enddate: ");
            DateTime enddate = DateTime.Parse(Console.ReadLine());
            List<Payroll> payroll = _payrollServices.GetPayrollsForPeriod(startdate, enddate);
            foreach (Payroll item in payroll)
            {
                Console.WriteLine(item);
            }
        }

        private void GenerateFinancialReport()
        {
            bool backToMenu = false;

            while (!backToMenu)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("------------- Financial Reporting Menu ----------");
                Console.WriteLine("1. Add Financial Record");
                Console.WriteLine("2. Get Financial Record by ID");
                Console.WriteLine("3. Get Financial Records for Employee");
                Console.WriteLine("4. Get Financial Records for Date");
                Console.WriteLine("5. Back to Main Menu");
                Console.ResetColor();   
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        addfinancialrecord();
                        break;

                    case "2":
                        getfinancialrecordbyid();
                        break;

                    case "3":
                        getfinancialrecordforemployee();
                        break;

                    case "4":
                        getfinancialrecordfordate();
                        break;

                    case "5":
                        backToMenu = true;
                        Console.WriteLine("Returning to Main Menu...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        void addfinancialrecord()
        {
            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Record Type (e.g., Income, Expense): ");
            string recordType = Console.ReadLine();
            _financialService.AddFinancialRecord(employeeId, description, amount, recordType);
            Console.WriteLine("Financial record added successfully.");
        }
        void getfinancialrecordbyid()
        {
            Console.Write("Enter Financial Record ID: ");
            int recordId = int.Parse(Console.ReadLine());
            FinancialRecord financialrecord = _financialService.GetFinancialRecordById(recordId);

            if (financialrecord != null)
            {
                Console.WriteLine("Financial Record Details:");
                Console.WriteLine($"Employee ID: {financialrecord.EmployeeID}");
                Console.WriteLine($"Record Date: {financialrecord.RecordDate}");
                Console.WriteLine($"Description: {financialrecord.Description}");
                Console.WriteLine($"Amount: {financialrecord.Amount}");
                Console.WriteLine($"Record Type: {financialrecord.RecordType}");
            }
            else
            {
                Console.WriteLine("No financial record found with the given ID.");
            }
        }

        void getfinancialrecordforemployee()
        {
            Console.Write("Enter Employee ID: ");
            int empId = int.Parse(Console.ReadLine());
            _financialService.GetFinancialRecordsForEmployee(empId);
        }

        void getfinancialrecordfordate()
        {
            Console.Write("Enter Record Date (YYYY-MM-DD): ");
            DateTime recordDate = DateTime.Parse(Console.ReadLine());
            _financialService.GetFinancialRecordsForDate(recordDate);
        }


        public void TaxCalculation()
        {
            bool backToMenu = false;

            while (!backToMenu)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("------------- Tax Calculation Menu -------------");
                Console.WriteLine("1. Calculate Tax");
                Console.WriteLine("2. Get Tax by ID");
                Console.WriteLine("3. Get Tax by Employee");
                Console.WriteLine("4. Get Tax for Year");
                Console.WriteLine("5. Get Net Salary for employee for a particular year");
                Console.WriteLine("6. Back to Main Menu");
                Console.ResetColor();   
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        calculatetax();
                        break;
                        

                    case "2":
                        gettaxbyid();
                        break;

                    case "3":
                        gettaxbyemployee();
                        break;

                    case "4":
                        gettaxforyear();
                        break;
                    case "5":
                        getnetsalary();
                        break;

                    case "6":
                        backToMenu = true;
                        Console.WriteLine("Returning to Main Menu...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

        }

        void calculatetax()
        {
                Console.WriteLine("Enter Employee Id: ");
                int empid = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter tax year: ");
                int year = int.Parse(Console.ReadLine());
                decimal total_tax = _taxServices.CalculateTax(empid, year);
                Console.WriteLine($"Toal Tax is:  {total_tax}");
        }

        void gettaxbyid()
        {
            Console.WriteLine("Enter TaxId: ");
            int taxId=int.Parse(Console.ReadLine());
            _taxServices.GetTaxById(taxId);
        }

        void gettaxbyemployee()
        {
            Console.WriteLine("Enter EmployeeId: ");
            int empid = int.Parse(Console.ReadLine());
            _taxServices.GetTaxesForEmployee(empid);
        }

        void gettaxforyear()
        {
            Console.WriteLine("Enter TaxYear: ");
            int taxyear = int.Parse(Console.ReadLine());
            _taxServices.GetTaxesForYear(taxyear);
        }
        void getnetsalary()
        {
            Console.WriteLine("Enter Employee ID: ");
            int empid=int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Year");
            int year=int.Parse(Console.ReadLine());
            decimal netsalary=_taxServices.Netsalary(empid, year);
            Console.WriteLine($"Net Salary of the Employee with Employee ID: {empid} is : {netsalary} ");
        }

    }

}
