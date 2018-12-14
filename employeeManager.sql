
CREATE DATABASE EmployeeManager;

use EmployeeManager;

CREATE TABLE employees (

    e_id int NOT NULL,
    fname varchar(100) NOT NULL,
    lname varchar(100) NOT NULL,
    dept_id varchar(100) NOT NULL,
    dateOFHire DATE NOT NULL,
	contract bool NOT NULL,
   
   PRIMARY KEY (e_id)
);

CREATE TABLE projects (

    p_id int NOT NULL,
    p_descr varchar(500) NOT NULL,
    
    PRIMARY KEY (p_id)
);

CREATE TABLE employeeProjects (

    e_id int NOT NULL,
    p_id int NOT NULL,
    
     FOREIGN KEY (e_id) REFERENCES employees(e_id),
	 FOREIGN KEY (p_id) REFERENCES projects(p_id)
);

SELECT * FROM employees;

INSERT INTO employees (e_id, fname, lname, dept_id, dateOfHire, contract) 
	values
		(1, 'Ken', 'Gitlitz', 'EN001', '2003-12-03', false),				
		(2, 'Jon', 'Kelly', 'SA001', '2010-01-07', false),				
		(3, 'Bill', 'Tenny', 'SA001', '1999-05-20', false),			
		(4, 'Paul', 'Martin', 'EN001', '2012-08-09', false),				
		(5, 'Jane', 'Samson', 'EN001', '2000-01-02', false),				
		(6, 'Karen', 'Martin', 'AD001', '2005-07-01', false);	
        
        
SELECT * FROM projects;

INSERT INTO projects (p_id, p_descr) 
	values
		(100, 'Database Interface'),				
		(101, 'Website'),				
		(102, 'New business process');		
	
                
		

    

