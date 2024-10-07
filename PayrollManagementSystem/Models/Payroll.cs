using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Models
{
    internal class Payroll
    {
        public int PayrollID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime PayPeriodStartDate { get; set; }
        public DateTime PayPeriodEndDate { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }

        public Payroll(int payrollID, int employeeID, DateTime payPeriodStartDate, DateTime payPeriodEndDate,
                       decimal basicSalary, decimal overtimePay, decimal deductions)
        {
            PayrollID = payrollID;
            EmployeeID = employeeID;
            PayPeriodStartDate = payPeriodStartDate;
            PayPeriodEndDate = payPeriodEndDate;
            BasicSalary = basicSalary;
            OvertimePay = overtimePay;
            Deductions = deductions;
            NetSalary = BasicSalary + OvertimePay - Deductions;
        }

        public Payroll()
        {
        }

        public override string ToString()
        {
            return $"Payroll ID: {PayrollID}, Employee ID: {EmployeeID}, Pay Period: {PayPeriodStartDate.ToShortDateString()} to {PayPeriodEndDate.ToShortDateString()}, " +
                   $"Net Salary: {NetSalary}";
        }
    }
}
