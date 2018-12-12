using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using EmployeeModel;
using ConfigurationFile;

namespace ProjectRepository
{

    public interface IProjectRepository<T>
    {
        List<project> FindAll();            // Get all projects				** Required project 
        //FindByID(string id);		// Find project by ID 
        List<employee> FindAllEmployees(string projID);  // employees on a given project			** Required
        bool Add(string empID, string projID);    // add employee to project			** Required
        bool Remove(string empID, string projID); // remove employee from project		** Required

        //List<project> FindAll(employee e);  // projects assigned to an employee		** Extra credit
        //bool Remove(employee e);    // remove employee from employeeProject table  	** Extra credit		
        //bool Remove(project p);	// Remove project from employeeProject table		** Extra credit    
        //bool Add(project x);        // Add project 						** Extra credit
        //bool Update(project x);     // Modify project					** Extra credit
        //bool Remove(project x);		// Remove project					** Extra credit

    }
    public class projectRepository : IProjectRepository<project>
    {
        private string connectionKey;

        public projectRepository()
        {
            connectionKey = configurationFile.getSetting("EmployeeProjConnectionString");
        }

       public  List<project> FindAll()
        {
            List<project> projects = new List<project>();

            // Data Provider DbConnection object
            SqlConnection dataConnection;

            // Data Provider DbCommand object
            SqlCommand dataCommand = new SqlCommand();

            // Data Provider DbDataReader object
            SqlDataReader dataReader;


            dataConnection = new SqlConnection(connectionKey);


            dataConnection.Open();
            dataCommand.Connection = dataConnection;
            dataCommand.CommandText = String.Format("SELECT * FROM projects");

            dataReader = dataCommand.ExecuteReader();

            while (dataReader.Read())
            {
                project proj = new project((int)dataReader["p_id"],
                     dataReader["p_descr"].ToString());


               projects.Add(proj);

            } // end read loop

            return projects;
        }

        public List<employee> FindAllEmployees(string projID)
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
            dataCommand.CommandText = String.Format("SELECT e_id, fname, lname, dept_id, dateOfHire, contract FROM employees WHERE employees.e_id IN(SELECT employeeProjects.e_id FROM employeeProjects WHERE employeeProjects.p_id = @P_ID)");
            dataCommand.Parameters.Add(new SqlParameter("@P_ID", projID));

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

       public bool Add(string empID, string projID)
        {

            // Data Provider DbConnection object
            SqlConnection dataConnection;

            // Data Provider DbCommand object
            SqlCommand dataCommand = new SqlCommand();

            // Data Provider DbDataReader object
            SqlDataReader dataReader;


            dataConnection = new SqlConnection(connectionKey);


            dataConnection.Open();
            dataCommand.Connection = dataConnection;
            try
            {
                dataCommand.CommandText = String.Format("INSERT INTO employeeProjects VALUES(@E_ID, @P_ID)");
                dataCommand.Parameters.Add(new SqlParameter("@E_ID", empID));
                dataCommand.Parameters.Add(new SqlParameter("@P_ID", projID));

                dataReader = dataCommand.ExecuteReader();
            }
            catch (SqlException)
            {
                return false;
            }

            return true;

        }

        public bool Remove(string empID, string projID)
        {

            // Data Provider DbConnection object
            SqlConnection dataConnection;

            // Data Provider DbCommand object
            SqlCommand dataCommand = new SqlCommand();

            // Data Provider DbDataReader object
            SqlDataReader dataReader;


            dataConnection = new SqlConnection(connectionKey);

            try
            {
                dataConnection.Open();
                dataCommand.Connection = dataConnection;
                dataCommand.CommandText = String.Format("DELETE FROM employeeProjects WHERE(employeeProjects.e_id = @E_ID AND employeeProjects.p_id = @P_ID)");
                dataCommand.Parameters.Add(new SqlParameter("@E_ID", empID));
                dataCommand.Parameters.Add(new SqlParameter("@P_ID", projID));

                dataReader = dataCommand.ExecuteReader();
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }


    }

}


