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
    public class EmployeeRepository : IEmployeeRepository
    {
        SqlCommand cmd = null;
        public EmployeeRepository()
        {
            cmd = new SqlCommand();
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
                {
                    cmd.CommandText = "Select * from Employee";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = (int)reader["emp_id"];
                        employee.FirstName = (string)reader["first_name"];
                        employee.LastName = (string)reader["last_name"];
                        employee.DateOfBirth = (DateTime)reader["dob"];
                        employee.Gender = (string)reader["gender"];
                        employee.Email = (string)reader["email"];
                        employee.PhoneNumber = (string)reader["phone"];
                        employee.Address = (string)reader["addresss"];
                        employee.Position = (string)reader["job_title"];
                        employee.JoiningDate = (DateTime)reader["join_date"];
                        employee.TerminationDate = reader["termination_date"] != DBNull.Value ? (DateTime?)reader["termination_date"] : null;
                        employees.Add(employee);
                    }
                    sqlConnection.Close();
                    return employees;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("An error occurred while fetching employees.", ex);
            }
            return employees;
        }

        public int AddEmployee(Employee employeeData)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.CommandText = "Insert into Employee values(@first_name,@last_name,@dob,@gender,@email,@phone,@addresss,@job_title,@join_date,@termination_date)";
                cmd.Parameters.AddWithValue("@first_name", employeeData.FirstName);
                cmd.Parameters.AddWithValue("@last_name", employeeData.LastName);
                cmd.Parameters.AddWithValue("@dob", employeeData.DateOfBirth);
                cmd.Parameters.AddWithValue("@gender", employeeData.Gender);
                cmd.Parameters.AddWithValue("@email", employeeData.Email);
                cmd.Parameters.AddWithValue("@phone", employeeData.PhoneNumber);
                cmd.Parameters.AddWithValue("@addresss", employeeData.Address);
                cmd.Parameters.AddWithValue("@job_title", employeeData.Position);
                cmd.Parameters.AddWithValue("@join_date", employeeData.JoiningDate);
                if (employeeData.TerminationDate == null)
                {
                    cmd.Parameters.AddWithValue("@termination_date", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@termination_date", employeeData.TerminationDate);
                }
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int addpemployeestatus=cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return addpemployeestatus;
            }

        }

        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = null;
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.CommandText="Select * from Employee where emp_id=@Employeeid";
                cmd.Parameters.AddWithValue("@Employeeid", employeeId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employee = new Employee();
                    employee.EmployeeID = (int)reader["emp_id"];
                    employee.FirstName = (string)reader["first_name"];
                    employee.LastName = (string)reader["last_name"];
                    employee.DateOfBirth = (DateTime)reader["dob"];
                    employee.Gender = (string)reader["gender"];
                    employee.Email = (string)reader["email"];
                    employee.PhoneNumber = (string)reader["phone"];
                    employee.Address = (string)reader["addresss"];
                    employee.Position = (string)reader["job_title"];
                    employee.JoiningDate = (DateTime)reader["join_date"];
                    employee.TerminationDate = reader["termination_date"] != DBNull.Value ? (DateTime?)reader["termination_date"] : null;
                }
                sqlConnection.Close();
                return employee;
            }
        }

        public int RemoveEmployee(int employeeId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "Delete from Employee where emp_id=@EmployeeId";
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int deleteemployeestatus=cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return deleteemployeestatus;

            }


        }

        public int UpdateEmployee(Employee employeeData)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConutil.GetConnString()))
            {
                cmd.Parameters.Clear(); cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE Employee SET first_name = @FirstName, last_name = @LastName, email = @Email WHERE emp_id = @Employeeid";
                cmd.Parameters.AddWithValue("@Employeeid", employeeData.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", employeeData.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employeeData.LastName);
                cmd.Parameters.AddWithValue("@Email", employeeData.Email);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int updateemployeestatus = cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return updateemployeestatus;
            }
        }
    }
}
