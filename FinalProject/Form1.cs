using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EmployeeModel;
using EmployeeRepository;
using EmployeeServices;
using ProjectRepository;
using ProjectServices;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        public string idToFind;
        public string projID;


        public ColumnHeader idHeader;
        public ColumnHeader dateHeader;
        public ColumnHeader depHeader;
        public ColumnHeader firstNameHeader;
        public ColumnHeader lastNameHeader;
        public ColumnHeader contractHeader;

        public ColumnHeader projIdHeader;
        public ColumnHeader projDesHeader;

        public Form1()
        {
            InitializeComponent();

            addButton.Enabled = false;
            removeButton.Enabled = false;

            employeeList.View = View.Details;
            employeeList.GridLines = true;
            employeeList.FullRowSelect = true;
            employeeList.MultiSelect = false;

            projectList.Activation = ItemActivation.OneClick;
            projectList.View = View.Details;
            projectList.GridLines = true;
            projectList.FullRowSelect = true;
            projectList.MultiSelect = false;

            projEmployeeList.View = View.Details;
            projEmployeeList.GridLines = true;
            projEmployeeList.FullRowSelect = true;
            projEmployeeList.MultiSelect = false;

            idHeader = employeeList.Columns.Add("ID");
            firstNameHeader = employeeList.Columns.Add("First Name");
            lastNameHeader = employeeList.Columns.Add("Last Name");
            depHeader = employeeList.Columns.Add("Department ID");
            dateHeader = employeeList.Columns.Add("Date of Hire");
            contractHeader = employeeList.Columns.Add("Contract");

            projIdHeader = projectList.Columns.Add("ID");
            projDesHeader = projectList.Columns.Add("Description");

            idHeader = projEmployeeList.Columns.Add("ID");
            firstNameHeader = projEmployeeList.Columns.Add("First Name");
            lastNameHeader = projEmployeeList.Columns.Add("Last Name");
            depHeader = projEmployeeList.Columns.Add("Department ID");
            dateHeader = projEmployeeList.Columns.Add("Date of Hire");
            contractHeader = projEmployeeList.Columns.Add("Contract");

            employeeList.Columns[0].Width = -2;
            employeeList.Columns[1].Width = -2;
            employeeList.Columns[2].Width = -2;
            employeeList.Columns[3].Width = -2;
            employeeList.Columns[4].Width = -2;
            employeeList.Columns[5].Width = -2;

            projectList.Columns[0].Width = -2;
            projectList.Columns[1].Width = -2;

            projEmployeeList.Columns[0].Width = -2;
            projEmployeeList.Columns[1].Width = -2;
            projEmployeeList.Columns[2].Width = -2;
            projEmployeeList.Columns[3].Width = -2;
            projEmployeeList.Columns[4].Width = -2;
            projEmployeeList.Columns[5].Width = -2;


            employeeRepo empRepo = new employeeRepo();

            employeeService empService = new employeeService(empRepo);

            List<employeeViewModel> employees = empService.getAll();


            projectRepository projRepo = new projectRepository();

            projectService projService = new projectService(projRepo);

            List<projectViewModel> projects = projService.getAll();


            foreach (employeeViewModel au in employees)
            {
                ListViewItem lvi;

                lvi = new ListViewItem(au.ID);

                lvi.SubItems.Add(au.FirstName);
                lvi.SubItems.Add(au.LastName);
                lvi.SubItems.Add(au.Department);
                lvi.SubItems.Add(au.dateOFHire);
                lvi.SubItems.Add(au.Contract);
                employeeList.Items.Add(lvi);

            }

            foreach (projectViewModel au in projects)
            {
                ListViewItem lvi;

                lvi = new ListViewItem(au.ID);

                lvi.SubItems.Add(au.Description);
                projectList.Items.Add(lvi);

            }

            projectList.ItemActivate += new EventHandler(ProjectClick);

        }

        private void ProjectClick(object sender, EventArgs e)
        {
            projEmployeeList.Clear();

            projEmployeeList.View = View.Details;
            projEmployeeList.GridLines = true;
            projEmployeeList.FullRowSelect = true;
            projEmployeeList.MultiSelect = false;

            idHeader = projEmployeeList.Columns.Add("ID");
            firstNameHeader = projEmployeeList.Columns.Add("First Name");
            lastNameHeader = projEmployeeList.Columns.Add("Last Name");
            depHeader = projEmployeeList.Columns.Add("Department ID");
            dateHeader = projEmployeeList.Columns.Add("Date of Hire");
            contractHeader = projEmployeeList.Columns.Add("Contract");

            projEmployeeList.Columns[0].Width = -2;
            projEmployeeList.Columns[1].Width = -2;
            projEmployeeList.Columns[2].Width = -2;
            projEmployeeList.Columns[3].Width = -2;
            projEmployeeList.Columns[4].Width = -2;
            projEmployeeList.Columns[5].Width = -2;

             projID = projectList.SelectedItems[0].SubItems[0].Text;

            projectRepository projRepo = new projectRepository();

            projectService projService = new projectService(projRepo);

            List<employeeViewModel> employees = projService.getAllEmployees(projID);


            foreach (employeeViewModel au in employees)
            {
                ListViewItem lvi;

                lvi = new ListViewItem(au.ID);

                lvi.SubItems.Add(au.FirstName);
                lvi.SubItems.Add(au.LastName);
                lvi.SubItems.Add(au.Department);
                lvi.SubItems.Add(au.dateOFHire);
                lvi.SubItems.Add(au.Contract);
                projEmployeeList.Items.Add(lvi);

            }

            addButton.Enabled = true;
            removeButton.Enabled = true;


        }

        private void findButton_Click(object sender, EventArgs e)
        {
           employeeRepo empRepo = new employeeRepo();

            idToFind = findBox.Text.Trim();

            if (idToFind == null)
            {
                MessageBox.Show("One or more fields missing valid input.");
                return;
            }

            if (string.IsNullOrWhiteSpace(idToFind))
            {
                MessageBox.Show("One or more fields missing valid input.");
                findBox.Text = null;
                return;
            }

            List<employee> foundEmployee = empRepo.FindByID(idToFind);

            employeeList.Clear();

            employeeList.View = View.Details;
            employeeList.GridLines = true;
            employeeList.FullRowSelect = true;
            employeeList.MultiSelect = false;

            idHeader = employeeList.Columns.Add("ID");
            firstNameHeader = employeeList.Columns.Add("First Name");
            lastNameHeader = employeeList.Columns.Add("Last Name");
            depHeader = employeeList.Columns.Add("Department ID");
            dateHeader = employeeList.Columns.Add("Date of Hire");
            contractHeader = employeeList.Columns.Add("Contract");

            ListViewItem lvi;
            try
            {
                lvi = new ListViewItem(foundEmployee[0].id.ToString());
            }
            catch (Exception)
            {

                employeeRepo empRepo2 = new employeeRepo();

                employeeService empService = new employeeService(empRepo2);

                List<employeeViewModel> employees = empService.getAll();

                foreach (employeeViewModel au in employees)
                { 

                    lvi = new ListViewItem(au.ID);

                    lvi.SubItems.Add(au.FirstName);
                    lvi.SubItems.Add(au.LastName);
                    lvi.SubItems.Add(au.Department);
                    lvi.SubItems.Add(au.dateOFHire);
                    lvi.SubItems.Add(au.Contract);
                    employeeList.Items.Add(lvi);

                }

                employeeList.Columns[0].Width = -2;
                employeeList.Columns[1].Width = -2;
                employeeList.Columns[2].Width = -2;
                employeeList.Columns[3].Width = -2;
                employeeList.Columns[4].Width = -2;
                employeeList.Columns[5].Width = -2;

                return;
            }

            lvi.SubItems.Add(foundEmployee[0].firstName);
            lvi.SubItems.Add(foundEmployee[0].lastName);
            lvi.SubItems.Add(foundEmployee[0].department);
            lvi.SubItems.Add(foundEmployee[0].dateOfHire.ToString());

            if (foundEmployee[0].contract == true)
            {
                lvi.SubItems.Add("Yes");
            }
            if (foundEmployee[0].contract == false)
            {
                lvi.SubItems.Add("No");
            }

            employeeList.Items.Add(lvi);

            employeeList.Columns[0].Width = -2;
            employeeList.Columns[1].Width = -2;
            employeeList.Columns[2].Width = -2;
            employeeList.Columns[3].Width = -2;
            employeeList.Columns[4].Width = -2;
            employeeList.Columns[5].Width = -2;



        }

        private void findBox_TextChanged(object sender, EventArgs e)
        {
            idToFind = findBox.Text;
        }

        private void findBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyPressed = e.KeyChar;
            if (Char.IsNumber(keyPressed) || keyPressed == (Char)(Keys.Back))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            employeeList.Clear();

            employeeList.View = View.Details;
            employeeList.GridLines = true;
            employeeList.FullRowSelect = true;
            employeeList.MultiSelect = false;

            idHeader = employeeList.Columns.Add("ID");
            firstNameHeader = employeeList.Columns.Add("First Name");
            lastNameHeader = employeeList.Columns.Add("Last Name");
            depHeader = employeeList.Columns.Add("Department ID");
            dateHeader = employeeList.Columns.Add("Date of Hire");
            contractHeader = employeeList.Columns.Add("Contract");

            employeeRepo empRepo = new employeeRepo();

            employeeService empService = new employeeService(empRepo);

            List<employeeViewModel> employees = empService.getAll();

            foreach (employeeViewModel au in employees)
            {
                ListViewItem lvi;

                lvi = new ListViewItem(au.ID);

                lvi.SubItems.Add(au.FirstName);
                lvi.SubItems.Add(au.LastName);
                lvi.SubItems.Add(au.Department);
                lvi.SubItems.Add(au.dateOFHire);
                lvi.SubItems.Add(au.Contract);
                employeeList.Items.Add(lvi);

                employeeList.Columns[0].Width = -2;
                employeeList.Columns[1].Width = -2;
                employeeList.Columns[2].Width = -2;
                employeeList.Columns[3].Width = -2;
                employeeList.Columns[4].Width = -2;
                employeeList.Columns[5].Width = -2;

            }

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                projectRepository projRepo = new projectRepository();

                projRepo.Add(employeeList.SelectedItems[0].SubItems[0].Text, projID);

                projEmployeeList.Clear();

                projEmployeeList.View = View.Details;
                projEmployeeList.GridLines = true;
                projEmployeeList.FullRowSelect = true;
                projEmployeeList.MultiSelect = false;

                idHeader = projEmployeeList.Columns.Add("ID");
                firstNameHeader = projEmployeeList.Columns.Add("First Name");
                lastNameHeader = projEmployeeList.Columns.Add("Last Name");
                depHeader = projEmployeeList.Columns.Add("Department ID");
                dateHeader = projEmployeeList.Columns.Add("Date of Hire");
                contractHeader = projEmployeeList.Columns.Add("Contract");

                projEmployeeList.Columns[0].Width = -2;
                projEmployeeList.Columns[1].Width = -2;
                projEmployeeList.Columns[2].Width = -2;
                projEmployeeList.Columns[3].Width = -2;
                projEmployeeList.Columns[4].Width = -2;
                projEmployeeList.Columns[5].Width = -2;

                projID = projectList.SelectedItems[0].SubItems[0].Text;

                projectService projService = new projectService(projRepo);

                List<employeeViewModel> employees = projService.getAllEmployees(projID);


                foreach (employeeViewModel au in employees)
                {
                    ListViewItem lvi;

                    lvi = new ListViewItem(au.ID);

                    lvi.SubItems.Add(au.FirstName);
                    lvi.SubItems.Add(au.LastName);
                    lvi.SubItems.Add(au.Department);
                    lvi.SubItems.Add(au.dateOFHire);
                    lvi.SubItems.Add(au.Contract);
                    projEmployeeList.Items.Add(lvi);

                }
            }
            catch (Exception)
            {
                return;
            }


        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {

                projectRepository projRepo = new projectRepository();

                projRepo.Remove(projEmployeeList.SelectedItems[0].SubItems[0].Text, projID);

                projEmployeeList.Clear();

                projEmployeeList.View = View.Details;
                projEmployeeList.GridLines = true;
                projEmployeeList.FullRowSelect = true;
                projEmployeeList.MultiSelect = false;

                idHeader = projEmployeeList.Columns.Add("ID");
                firstNameHeader = projEmployeeList.Columns.Add("First Name");
                lastNameHeader = projEmployeeList.Columns.Add("Last Name");
                depHeader = projEmployeeList.Columns.Add("Department ID");
                dateHeader = projEmployeeList.Columns.Add("Date of Hire");
                contractHeader = projEmployeeList.Columns.Add("Contract");

                projEmployeeList.Columns[0].Width = -2;
                projEmployeeList.Columns[1].Width = -2;
                projEmployeeList.Columns[2].Width = -2;
                projEmployeeList.Columns[3].Width = -2;
                projEmployeeList.Columns[4].Width = -2;
                projEmployeeList.Columns[5].Width = -2;

                projID = projectList.SelectedItems[0].SubItems[0].Text;

                projectService projService = new projectService(projRepo);

                List<employeeViewModel> employees = projService.getAllEmployees(projID);


                foreach (employeeViewModel au in employees)
                {
                    ListViewItem lvi;

                    lvi = new ListViewItem(au.ID);

                    lvi.SubItems.Add(au.FirstName);
                    lvi.SubItems.Add(au.LastName);
                    lvi.SubItems.Add(au.Department);
                    lvi.SubItems.Add(au.dateOFHire);
                    lvi.SubItems.Add(au.Contract);
                    projEmployeeList.Items.Add(lvi);

                }
            }
            catch(Exception)
            {
                return;
            }
        }
    }
}
