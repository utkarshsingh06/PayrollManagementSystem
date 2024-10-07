using PayrollManagementSystem.Exceptions;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Services
{
    public class TaxServices : ITaxServices
    {
        readonly ITaxRepository _taxRepository;
        public TaxServices(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }
        public decimal CalculateTax(int employeeId, int taxYear)
        {
            try
            {
                decimal totaltax = _taxRepository.CalculateTax(employeeId, taxYear);
            return totaltax;
            }
            catch (InvalidInputException ex1)
            {
                Console.WriteLine($"Error: {ex1.Message}");
                return 0;
            }
            catch (TaxCalculationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }

        }

        public void GetTaxById(int taxId)
        {
            Tax tax=_taxRepository.GetTaxById(taxId);
            Console.WriteLine(tax.ToString());
        }

        public void GetTaxesForEmployee(int employeeId)
        {
            List<Tax> taxlist = _taxRepository.GetTaxesForEmployee(employeeId);
            foreach (Tax tax in taxlist)
            {
                Console.WriteLine(tax.ToString());
            }
        }

        public void  GetTaxesForYear(int taxYear)
        {
            List<Tax> taxlist = _taxRepository.GetTaxesForYear(taxYear);
            foreach (Tax tax in taxlist)
            {
                Console.WriteLine(tax.ToString());
            }
        }

        public decimal Netsalary(int employeeid, int year)
        {
            return _taxRepository.Netsalary(employeeid, year);
        }
    }
}
