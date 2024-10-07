using PayrollManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Services
{
    public interface ITaxServices
    {
        decimal CalculateTax(int employeeId, int taxYear);
        void GetTaxById(int taxId);
        void GetTaxesForEmployee(int employeeId);
        void  GetTaxesForYear(int taxYear);
        decimal Netsalary(int employeeid, int year);
    }
}
