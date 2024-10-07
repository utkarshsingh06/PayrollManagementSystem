using PayrollManagementSystem.Exceptions;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repository.Interfaces;
using PayrollManagementSystem.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Repository
{
    public class TaxRepository : ITaxRepository
    {
        SqlCommand cmd = null;
        public TaxRepository()
        {
            cmd = new SqlCommand();
        }
        public decimal CalculateTax(int employeeId, int taxYear)
        {
            if (employeeId <= 0)
            {
                throw new InvalidInputException("Employee ID must be greater than zero.");

            }
            if (taxYear < 2000  ||  taxYear > DateTime.Now.Year)
            {
                throw new TaxCalculationException("Tax Year Must be in range 2000 and current year");
            }
            decimal taxRate = 0.1m;
            decimal totalTaxableIncome = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT SUM(basic_salary + overtimepay - deductions) AS TotalTaxableIncome FROM payrolltb1 WHERE emp_id = @EmployeeID AND YEAR(payperiod_start) = @TaxYear";
                    cmd.Connection = sqlConnection;
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@TaxYear", taxYear);

                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        totalTaxableIncome = reader["TotalTaxableIncome"] != DBNull.Value ? (decimal)reader["TotalTaxableIncome"] : 0;
                    }

                    sqlConnection.Close();
                    cmd.Parameters.Clear();
                }

                if (totalTaxableIncome == 0)
                {
                    return 0;
                }
                decimal taxAmount = totalTaxableIncome * taxRate;
                using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO Tax (emp_id, tax_year, taxable_income, tax_amount) VALUES (@EmployeeID, @TaxYear, @TaxableIncome, @TaxAmount)";
                    cmd.Connection = sqlConnection;
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                    cmd.Parameters.AddWithValue("@TaxableIncome", totalTaxableIncome);
                    cmd.Parameters.AddWithValue("@TaxAmount", taxAmount);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }

                return taxAmount;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("An error occurred while calculating the tax.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while calculating the tax.", ex);
            }
        }

        public Tax GetTaxById(int taxId)
        {
            Tax tax = null;
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.CommandText = "Select * from Tax where tax_id=@Taxid";
                cmd.Parameters.AddWithValue("@Taxid", taxId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tax = new Tax();
                    {
                        tax.EmployeeID = (int)reader["emp_id"];
                        tax.TaxYear = (int)reader["tax_year"];
                        tax.TaxableIncome = (decimal)reader["taxable_income"];
                        tax.TaxAmount = (decimal)reader["tax_amount"];
                    };
                }
                sqlConnection.Close();
                return tax;
            }
        }

        public List<Tax> GetTaxesForEmployee(int employeeId)
        {
            List<Tax> taxs = new List<Tax>();
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.CommandText = "Select * from Tax where emp_id=@EmpId";
                cmd.Parameters.AddWithValue("@EmpId", employeeId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tax tax = new Tax();
                    {
                        tax.EmployeeID = (int)reader["emp_id"];
                        tax.TaxYear = (int)reader["tax_year"];
                        tax.TaxableIncome = (decimal)reader["taxable_income"];
                        tax.TaxAmount = (decimal)reader["tax_amount"];
                        taxs.Add(tax);

                    };
                }
                sqlConnection.Close();
                return taxs;

            }
        }

        public List<Tax> GetTaxesForYear(int taxYear)
        {
            List<Tax> taxs = new List<Tax>();
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.CommandText = "Select * from Tax where tax_year=@taxyear";
                cmd.Parameters.AddWithValue("@taxyear", taxYear);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tax tax = new Tax();
                    {
                        tax.TaxID = (int)reader["tax_id"];
                        tax.EmployeeID = (int)reader["emp_id"];
                        tax.TaxYear = (int)reader["tax_year"];
                        tax.TaxableIncome = (decimal)reader["taxable_income"];
                        tax.TaxAmount = (decimal)reader["tax_amount"];
                        taxs.Add(tax);

                    };
                }
                sqlConnection.Close();
                return taxs;
            }
        }

        public decimal Netsalary(int employeeid, int year)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT (taxable_income - tax_amount) AS NetSalary FROM Tax WHERE emp_id = @EmployeeID AND tax_year = @TaxYear";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@EmployeeID", employeeid);
                cmd.Parameters.AddWithValue("@TaxYear", year);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                decimal netSalary = 0; 

                if (reader.Read())
                {
                    netSalary = reader["NetSalary"] != DBNull.Value ? (decimal)reader["NetSalary"] : 0;
                }

                sqlConnection.Close();
                cmd.Parameters.Clear();
                return netSalary;
            }
        }
    }
}
