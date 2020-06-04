using SMTRPZ_IT_company.Model;
using SMTRPZ_IT_company.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTRPZ_IT_company
{
    public class UnitOfWork : IDisposable
    {
        private LabContext context;
        private SQLDepartmentEmployeeRepository deprtmEmplRep;
        private SQLDepartmentRepository departRep;
        private SQLEmployeeRepository emplRep;
        private SQLTaskRepository taskRep;
        private bool disposed = false;

        public UnitOfWork()
        {
            context = new LabContext();
        }

        public SQLDepartmentEmployeeRepository departmentEmployeeRepository
        {

            get
            {

                if (deprtmEmplRep == null)
                {
                    deprtmEmplRep = new SQLDepartmentEmployeeRepository(context);
                }
                return deprtmEmplRep;
            }
        }
        public SQLDepartmentRepository departmentRepository
        {

            get
            {

                if (departRep == null)
                {
                    departRep = new SQLDepartmentRepository(context);
                }
                return departRep;
            }
        }
        public SQLEmployeeRepository employeeRepository
        {

            get
            {

                if (emplRep == null)
                {
                    emplRep = new SQLEmployeeRepository(context);
                }
                return emplRep;
            }
        }
        public SQLTaskRepository taskRepository
        {

            get
            {

                if (taskRep == null)
                {
                    taskRep = new SQLTaskRepository(context);
                }
                return taskRep;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();

            if (taskRep != null)
            {
                taskRep.Dispose();
            }
            if (emplRep != null)
            {
                emplRep.Dispose();
            }
            if (departRep != null)
            {
                departRep.Dispose();
            }
            if (deprtmEmplRep != null)
            {
                deprtmEmplRep.Dispose();
            }

            GC.SuppressFinalize(this);
        }

    }
}
