using PayrollManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Repository.Interfaces
{
    public interface ITaxRepository
    {
        decimal CalculateTax(int employeeId, int taxYear);
        Tax GetTaxById(int taxId);
        List<Tax> GetTaxesForEmployee(int employeeId);
        List<Tax> GetTaxesForYear(int taxYear);
        decimal Netsalary(int employeeid, int year);


    }
}
