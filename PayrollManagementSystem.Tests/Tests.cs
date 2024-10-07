using NUnit.Framework;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repository;
using PayrollManagementSystem.Repository.Interfaces;
using PayrollManagementSystem.Services;
namespace PayrollManagementSystem.Tests
{
    [TestFixture]
    public class Tests
    {
        private IEmployeeService _employeeService;
        private IEmployeeRepository _employeeRepository;
        private ITaxServices _taxServices;
        private ITaxRepository _taxRepository;

        [SetUp]
        public void Setup()
        {
            _employeeRepository = new EmployeeRepository();
            _employeeService = new EmployeeService(_employeeRepository);
            _taxRepository = new TaxRepository();
            _taxServices = new TaxServices(_taxRepository);
        }

        [Test]
        public void AddEmployee_ShouldAddEmployeeSuccessfully()
        {
            var employee = new Employee
            {
                FirstName = "Jane",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "F",
                Email = "jane.doe@example.com",
                PhoneNumber = "0987654321",
                Address = "456 Elm St",
                Position = "Developer",
                JoiningDate = DateTime.Now,
                TerminationDate = null
            };
            int result = _employeeService.AddEmployee(employee);
            Assert.That(result, Is.EqualTo(1));
            var addedEmployee = _employeeRepository.GetAllEmployees().FirstOrDefault(e => e.Email == "jane.doe@example.com");
            Assert.That(addedEmployee, Is.Not.Null);
            Assert.That(addedEmployee.FirstName, Is.EqualTo("Jane"));
            Assert.That(addedEmployee.LastName, Is.EqualTo("Doe"));
        }
        [Test]
        public void CalculateNetSalaryAfterDeductions_ShouldReturnCorrectNetSalary()
        {
            var employeeId = 1;
            var taxYear = DateTime.Now.Year;

            decimal taxableIncome = 10800;
            decimal taxAmount = 1080;


            decimal netSalary = _taxServices.Netsalary(employeeId, taxYear);
    
            decimal expectedNetSalary = taxableIncome - taxAmount;

            // Assert
            Assert.That(netSalary, Is.EqualTo(expectedNetSalary).Within(0.01));

              
        }
    }

}
