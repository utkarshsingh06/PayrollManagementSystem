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
    internal class PayrollRepository : IPayrollTRepository
    {
        SqlCommand cmd = null;
        public PayrollRepository()
        {
            cmd = new SqlCommand();
        }
        public Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate,decimal basicsalary,decimal overtimepay,decimal deductions)
        {
            Payroll payroll = null;
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                    cmd.CommandText="INSERT INTO payrolltb1 (emp_id, payperiod_start, payperiod_end, basic_salary, overtimepay, deductions) VALUES (@EmployeeID, @PayPeriodStart, @PayPeriodEnd, @BasicSalary, @OvertimePay, @Deductions";
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        payroll = new Payroll();
                        payroll.PayrollID = (int)reader["payroll_id"];
                        payroll.EmployeeID = (int)reader["emp_id"];
                        payroll.PayPeriodStartDate = (DateTime)reader["payperiod_start"];
                        payroll.PayPeriodEndDate = (DateTime)reader["payperiod_end"];
                        payroll.BasicSalary = (decimal)reader["basic_salary"];
                        payroll.OvertimePay = (decimal)reader["overtimepay"];
                        payroll.Deductions = (decimal)reader["deductions"];
                        payroll.NetSalary = payroll.BasicSalary + payroll.OvertimePay - payroll.Deductions;
                    }
            }
            return payroll;
        }

        public Payroll GetPayrollById(int payrollId)
        {
            Payroll payroll= null;
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "Select * from payrolltb1 where payroll_id=@payrollid";
                cmd.Parameters.AddWithValue("@payrollid", payrollId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    payroll= new Payroll();
                    payroll.PayrollID = (int)reader["payroll_id"];
                    payroll.EmployeeID = (int)reader["emp_id"];
                    payroll.PayPeriodStartDate = (DateTime)reader["payperiod_start"];
                    payroll.PayPeriodEndDate = (DateTime)reader["payperiod_end"];
                    payroll.BasicSalary = (decimal)reader["basic_salary"];
                    payroll.OvertimePay = (decimal)reader["overtimepay"];
                    payroll.Deductions = (decimal)reader["deductions"];
                    payroll.NetSalary = payroll.BasicSalary + payroll.OvertimePay - payroll.Deductions;
                }
                sqlConnection.Close();
                return payroll;
            }
        }

        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            List<Payroll> payrolls = new List<Payroll>();
            Payroll payroll = null;

            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM payrolltb1 WHERE emp_id = @EmployeeID";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    payroll = new Payroll();
                    payroll.PayrollID = (int)reader["payroll_id"];
                    payroll.EmployeeID = (int)reader["emp_id"];
                    payroll.PayPeriodStartDate = (DateTime)reader["payperiod_start"];
                    payroll.PayPeriodEndDate = (DateTime)reader["payperiod_end"];
                    payroll.BasicSalary = (decimal)reader["basic_salary"];
                    payroll.OvertimePay = (decimal)reader["overtimepay"];
                    payroll.Deductions = (decimal)reader["deductions"];
                    payroll.NetSalary = payroll.BasicSalary + payroll.OvertimePay - payroll.Deductions;
                    payrolls.Add(payroll);
                }

                sqlConnection.Close();
            }

            return payrolls;
        }

        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrolls = new List<Payroll>();
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM payrolltb1 WHERE payperiod_start >= @StartDate AND payperiod_end <= @EndDate";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Payroll payroll = new Payroll();
                    payroll.PayrollID = (int)reader["payroll_id"];
                    payroll.EmployeeID = (int)reader["emp_id"];
                    payroll.PayPeriodStartDate = (DateTime)reader["payperiod_start"];
                    payroll.PayPeriodEndDate = (DateTime)reader["payperiod_end"];
                    payroll.BasicSalary = (decimal)reader["basic_salary"];
                    payroll.OvertimePay = (decimal)reader["overtimepay"];
                    payroll.Deductions = (decimal)reader["deductions"];
                    payroll.NetSalary = payroll.BasicSalary + payroll.OvertimePay - payroll.Deductions;
                    payrolls.Add(payroll);
                }

                sqlConnection.Close();
            }

            return payrolls;
        }
    }
}
