using PayrollManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Services
{
    internal interface IFinancialService
    {
        void AddFinancialRecord(int employeeId, string description, decimal amount, string recordType);
        FinancialRecord GetFinancialRecordById(int recordId);
        void GetFinancialRecordsForEmployee(int employeeId);
        void GetFinancialRecordsForDate(DateTime recordDate);
    }
}
