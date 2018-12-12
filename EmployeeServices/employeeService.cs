using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeRepository;
using EmployeeModel;

namespace EmployeeServices
{

    public static class EmployeerMapperExtensionMethods
    {
        public static employeeViewModel ConvertToViewModel(this employee employeeIn)
        {
            employeeViewModel avm = new employeeViewModel(employeeIn.id.ToString());

            avm.LastName = employeeIn.m_lname;
            avm.FirstName = employeeIn.m_fname;
            avm.Department = employeeIn.m_dept;
            avm.dateOFHire = employeeIn.m_dateOfHire.ToString();
            avm.Contract = employeeIn.m_contract ? "Yes" : "No";

            return avm;
        }
    }

    public class employeeViewModel
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string dateOFHire { get; set; }
        public string Contract { get; set; }

        public string Name { get { return FirstName + " " + LastName; } }

        public employeeViewModel(string id)
        {
            ID = id;
        }

        public employeeViewModel(string id, string lname, string fname, string department, string date, string contract)
        {

            LastName = lname;
            FirstName = fname;
            ID = id;
            Department = department;
            dateOFHire = date;
            Contract = contract;
        }
    }

    public class employeeService
    {
        IEmployeeRepository<employee> _repository;
        public employeeService(employeeRepo repo)
        {
            _repository = repo;
        }
        public List<employeeViewModel> getAll()
        {
            List<employeeViewModel> employees = new List<employeeViewModel>();
            List<employee> lstemployees;

            lstemployees = _repository.FindAll();

            foreach (employee em in lstemployees)
            {
                employees.Add(em.ConvertToViewModel());
            }

            return employees;
        }
    }
}
