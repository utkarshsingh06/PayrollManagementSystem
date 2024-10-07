using PayrollManagementSystem.Repository.Interfaces;
using PayrollManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Services
{
    internal class FinancialService : IFinancialService
    {
        readonly IFinancialRepository _financialRepository;
        public FinancialService(IFinancialRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }
        public void AddFinancialRecord(int employeeId, string description, decimal amount, string recordType)
        {
            int insertstatus =_financialRepository.AddFinancialRecord(employeeId, description, amount, recordType); 
            if (insertstatus > 0)
            {
                Console.WriteLine(insertstatus);
            }
            else
            {
                Console.WriteLine("error in insertion");
            }
        }

        public FinancialRecord GetFinancialRecordById(int recordId)
        {
           FinancialRecord financialrecord = _financialRepository.GetFinancialRecordById(recordId);
            return financialrecord;
            
        }

        public void GetFinancialRecordsForDate(DateTime recordDate)
        {
            List<FinancialRecord> finanrecords = _financialRepository.GetFinancialRecordsForDate(recordDate);
            foreach (FinancialRecord item in finanrecords)
            {
                Console.WriteLine(item);
            }

        }

        public void GetFinancialRecordsForEmployee(int employeeId)
        {
            List<FinancialRecord> finanrecords = _financialRepository.GetFinancialRecordsForEmployee(employeeId);
            foreach (FinancialRecord item in finanrecords)
            {
                Console.WriteLine(item);
            }
        }
    }
}
