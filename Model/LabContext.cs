using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SMTRPZ_IT_company.Model
{
    public class LabContext : DbContext
    {
       
            public DbSet<Department> Departments { get; set; }
            public DbSet<DepartmentEmployee> DepartmentsEmployees { get; set; }
            public DbSet<Employee> Employees { get; set; }
            public DbSet<EmplTask> Tasks { get; set; }


            public LabContext()
                : base("DefaultConnection")
            {
            }

            public static LabContext Create()
            {
                return new LabContext();
            }
        
    }
}