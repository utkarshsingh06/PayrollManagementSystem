using Microsoft.VisualBasic;
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
    internal class FinancialRepository : IFinancialRepository
    {
        SqlCommand cmd = null;
        public FinancialRepository()
        {
            cmd = new SqlCommand();
        }
        public int AddFinancialRecord(int employeeId, string description, decimal amount, string recordType)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO FinancialRecord VALUES (@emp_id,@record_date,@descriptionn,@amount,@record_type)";
                cmd.Parameters.AddWithValue("@emp_id", employeeId);
                cmd.Parameters.AddWithValue("@record_date",DateTime.Now);
                cmd.Parameters.AddWithValue("@descriptionn", description);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@record_type", recordType);

                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                sqlConnection.Close();

                return rowsAffected;
            }
        }

        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            FinancialRecord financialrecord = null;
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.CommandText = "Select * from FinancialRecord where record_id=@recordid";
                cmd.Parameters.AddWithValue("@recordid", recordId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    financialrecord = new FinancialRecord();
                    financialrecord.RecordID = (int)reader["record_id"];
                    financialrecord.EmployeeID = (int)reader["emp_id"];
                    financialrecord.RecordDate = (DateTime)reader["record_date"];
                    financialrecord.Description = (string)reader["descriptionn"];
                    financialrecord.Amount = (decimal)reader["amount"];
                    financialrecord.RecordType = (string)reader["record_type"];
                }
                sqlConnection.Close();
                return financialrecord;
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            List<FinancialRecord> financialrecord= new List<FinancialRecord>();
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.CommandText = "SELECT * FROM FinancialRecord WHERE record_date = @recorddate";
                cmd.Parameters.AddWithValue("@recorddate", recordDate);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FinancialRecord financialRecords = new FinancialRecord();
                    financialRecords.RecordID= (int)reader["record_id"];
                    financialRecords.EmployeeID = (int)reader["emp_id"];
                    financialRecords.RecordDate = (DateTime)reader["record_date"];
                    financialRecords.Description = (string)reader["descriptionn"];
                    financialRecords.Amount = (decimal)reader["amount"];
                    financialRecords.RecordType = (string)reader["record_type"];
                    financialrecord.Add(financialRecords);
                }
                sqlConnection.Close();
                return financialrecord;
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            List<FinancialRecord> financialrecords = new List<FinancialRecord>();
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.CommandText = "Select * from FinancialRecord where emp_id=@Empid";
                cmd.Parameters.AddWithValue("@Empid", employeeId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FinancialRecord financialrecord = new FinancialRecord();
                    financialrecord.RecordID = (int)reader["record_id"];
                    financialrecord.EmployeeID = (int)reader["emp_id"];
                    financialrecord.RecordDate = (DateTime)reader["record_date"];
                    financialrecord.Description = (string)reader["descriptionn"];
                    financialrecord.Amount = (decimal)reader["amount"];
                    financialrecord.RecordType = (string)reader["record_type"];
                    financialrecords.Add(financialrecord);
                }
                sqlConnection.Close();
                return financialrecords;
            }
        }
    }
}
