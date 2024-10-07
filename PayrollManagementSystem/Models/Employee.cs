using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public Employee(int employeeID, string firstName, string lastName, DateTime dateOfBirth, string gender,
                        string email, string phoneNumber, string address, string position, DateTime joiningDate)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Position = position;
            JoiningDate = joiningDate;
            TerminationDate = null;
        }

        public Employee()
        {
        }

        public override string ToString()
        {
            return $"Employee ID: {EmployeeID}, Name: {FirstName} {LastName}, DOB: {DateOfBirth}, Gender: {Gender}, " +
                   $"Position: {Position}, Email: {Email}, Joined: {JoiningDate.ToShortDateString()}";
        }
    }
}
