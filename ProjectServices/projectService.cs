using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectRepository;
using EmployeeModel;
using EmployeeServices;

namespace ProjectServices
{
    public static class projectrMapperExtensionMethods
    {
        public static projectViewModel ConvertToViewModel(this project projectIn)
        {
            projectViewModel avm = new projectViewModel(projectIn.id.ToString(), projectIn.description);
            return avm;
        }
    }

    public class projectViewModel
    {
        public string ID { get; set; }
        public string Description { get; set; }

        public projectViewModel(string id, string des)
        {
            ID = id;
            Description = des;
        }
    }

    public class projectService
    {
        IProjectRepository<project> _repository;
        public projectService(projectRepository repo)
        {
            _repository = repo;
        }

        public List<projectViewModel> getAll()
        {
            List<projectViewModel> projects = new List<projectViewModel>();
            List<project> lstprojects;

            lstprojects = _repository.FindAll();

            foreach (project proj in lstprojects)
            {
                projects.Add(proj.ConvertToViewModel());
            }

            return projects;
        }

        public List<employeeViewModel> getAllEmployees(string projID)
        {
            List<employeeViewModel> emps = new List<employeeViewModel>();
            List<employee> lstemps;

            lstemps = _repository.FindAllEmployees(projID);
            
            foreach (employee emp in lstemps)
            {
                emps.Add(emp.ConvertToViewModel());
            }

            return emps;
        }

    }
}
