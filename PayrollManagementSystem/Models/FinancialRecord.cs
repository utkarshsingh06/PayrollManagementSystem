using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Models
{
    internal class FinancialRecord
    {
        public int RecordID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime RecordDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string RecordType { get; set; } 

        public FinancialRecord(int recordID, int employeeID, DateTime recordDate, string description, decimal amount, string recordType)
        {
            RecordID = recordID;
            EmployeeID = employeeID;
            RecordDate = recordDate;
            Description = description;
            Amount = amount;
            RecordType = recordType;
        }

        public FinancialRecord()
        {
        }

        public override string ToString()
        {
            return $"Record ID: {RecordID}, Employee ID: {EmployeeID}, Date: {RecordDate.ToShortDateString()}, Amount: {Amount}, Type: {RecordType}, Description: {Description}";
        }
    }
}
