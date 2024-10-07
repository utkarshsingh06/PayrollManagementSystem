using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Services
{
    internal class PayrollServices : IpayrollServices
    {
        readonly IPayrollTRepository _payrollTRepository;
        public PayrollServices(IPayrollTRepository payrollTRepository)
        {
            _payrollTRepository = payrollTRepository;
        }
        public Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate, decimal basicsalary, decimal overtimepay, decimal deductions)
        {
            Payroll payroll = _payrollTRepository.GeneratePayroll(employeeId,startDate,endDate,basicsalary,overtimepay,deductions);
            return payroll;
        }

        public Payroll GetPayrollById(int payrollId)
        {
            Payroll payroll = _payrollTRepository.GetPayrollById(payrollId);
            return payroll;
        }

        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            List<Payroll> payroll = _payrollTRepository.GetPayrollsForEmployee(employeeId);
            return payroll;
        }

        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
           List<Payroll> payroll = _payrollTRepository.GetPayrollsForPeriod(startDate, endDate);
            return payroll;
        }
    }
}
