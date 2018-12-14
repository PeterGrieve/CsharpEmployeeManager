using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConfigurationFile;
using EmployeeModel;
using System.Data.SqlClient;

namespace EmployeeRepository
{
    public interface IEmployeeRepository<T>
    {
        List<employee> FindAll();   // Get all employees
        List<employee> FindByID(string id);   // Find employee by ID  
    }

    public class employeeRepo : IEmployeeRepository<employee>
    {
        private string connectionKey;

        public employeeRepo()
        {
            connectionKey = configurationFile.getSetting("EmployeeProjConnectionString");
        }


        public List<employee> FindAll()
        {
            List<employee> employees = new List<employee>();

            // Data Provider DbConnection object
            SqlConnection dataConnection;

            // Data Provider DbCommand object
            SqlCommand dataCommand = new SqlCommand();

            // Data Provider DbDataReader object
            SqlDataReader dataReader;


            dataConnection = new SqlConnection(connectionKey);


            dataConnection.Open();
            dataCommand.Connection = dataConnection;
            dataCommand.CommandText = String.Format("SELECT * FROM employees");

            dataReader = dataCommand.ExecuteReader();

            while (dataReader.Read())
            {
                employee emp = new employee((int)dataReader["e_id"],
                     dataReader["fname"].ToString(),
                     dataReader["lname"].ToString(),
                     dataReader["dept_id"].ToString(),
            (System.DateTime)dataReader["dateOfHire"],
                     (bool)dataReader["contract"]);

                employees.Add(emp);

            } // end read loop

            return employees;
        }

        public List<employee> FindByID(string id)
        {
            List<employee> employees = new List<employee>();

            // Data Provider DbConnection object
            SqlConnection dataConnection;

            // Data Provider DbCommand object
            SqlCommand dataCommand = new SqlCommand();

            // Data Provider DbDataReader object
            SqlDataReader dataReader;


            dataConnection = new SqlConnection(connectionKey);


            dataConnection.Open();

            dataCommand.Connection = dataConnection;
            dataCommand.CommandText = String.Format("SELECT * FROM employees WHERE e_id = @ID");
            dataCommand.Parameters.Add(new SqlParameter("@ID", id));

            dataReader = dataCommand.ExecuteReader();

            while (dataReader.Read())
            {
                employee emp = new employee((int)dataReader["e_id"],
                     dataReader["fname"].ToString(),
                     dataReader["lname"].ToString(),
                     dataReader["dept_id"].ToString(),
            (System.DateTime)dataReader["dateOfHire"],
                     (bool)dataReader["contract"]);

                employees.Add(emp);

            } // end read loop

            return employees;
        }

    }
}



