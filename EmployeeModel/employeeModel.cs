using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace EmployeeModel
{

    // NOTE: For this lab, the class is not abstract.  

    public class project
    {
        protected int m_id;
        protected string m_description;

        List<employee> _employees;

        public project(int id, string description)
        {
            m_id = id;
            m_description = description;

            _employees = new List<employee>();
        }

        public int id
        {
            get { return m_id; }
        }

        public string description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public List<employee> employees
        {
            get { return _employees; }
            set { _employees = value; }
        }
    }

    public class employee
    {
        protected int m_id;
        public string m_fname;
        public string m_lname;
        public DateTime m_dateOfHire;

        // future 
        public string m_dept;
        public bool m_contract;

        public employee(int id, string fname, string lname, string dept, DateTime dateOfHire, bool contract)
        {
            // business rule - id may never change 
            m_id = id;
            m_fname = fname;
            m_lname = lname;
            m_dept = dept;
            m_dateOfHire = dateOfHire;
            m_contract = contract;
        }

        public string lastName
        {
            get { return m_lname; }
            set { m_lname = value; }
        }

        public string firstName
        {
            get { return m_fname; }
            set { m_fname = value; }
        }

        public string name
        {
            get { return string.Format("{0} {1}", m_fname, m_lname); }
        }

        public int id
        {
            // business rule - id may never change, so readonly propertly
            get { return m_id; }
        }

        public DateTime dateOfHire
        {
            get { return m_dateOfHire; }
        }

        public string department
        {
            get { return m_dept; }
            set { m_dept = value; }
        }

        public bool contract
        {
            get { return m_contract; }
            set { m_contract = value; }
        }
    }
}