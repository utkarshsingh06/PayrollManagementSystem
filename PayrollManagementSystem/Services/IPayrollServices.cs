using PayrollManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Services
{
    internal interface IpayrollServices
    {
        public Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate, decimal basicsalary, decimal overtimepay, decimal deductions);
        public Payroll GetPayrollById(int payrollId);
        public List<Payroll> GetPayrollsForEmployee(int employeeId);
        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
    }
}
