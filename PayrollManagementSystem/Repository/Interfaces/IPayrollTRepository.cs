using PayrollManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Repository.Interfaces
{
    internal interface IPayrollTRepository
    {
        Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate, decimal basicsalary, decimal overtimepay, decimal deductions);
        Payroll GetPayrollById(int payrollId);
        List<Payroll> GetPayrollsForEmployee(int employeeId);
        List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
    }
}
