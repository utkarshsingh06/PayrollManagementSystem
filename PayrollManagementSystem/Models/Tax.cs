using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Models
{
    public class Tax
    {
        public int TaxID { get; set; }
        public int EmployeeID { get; set; }
        public int TaxYear { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal TaxAmount { get; set; }

        public Tax(int taxID, int employeeID, int taxYear, decimal taxableIncome, decimal taxAmount)
        {
            TaxID = taxID;
            EmployeeID = employeeID;
            TaxYear = taxYear;
            TaxableIncome = taxableIncome;
            TaxAmount = taxAmount;
        }

        public Tax()
        {
        }

        public override string ToString()
        {
            return $"Tax ID: {TaxID}, Employee ID: {EmployeeID}, Tax Year: {TaxYear}, Taxable Income: {TaxableIncome:C}, Tax Amount: {TaxAmount:C}";
        }
    }
}
