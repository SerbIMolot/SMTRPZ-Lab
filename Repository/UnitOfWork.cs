using SMTRPZ_IT_company.Model;
using SMTRPZ_IT_company.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTRPZ_IT_company
{
    public class UnitOfWork
    {
        private static LabContext context;
        private static SQLDepartmentEmployeeRepository deprtmEmplRep;
        private static SQLDepartmentRepository departRep;
        private static SQLEmployeeRepository emplRep;
        private static SQLTaskRepository taskRep;
        private static bool disposed = false;
        private static UnitOfWork instance;

        private UnitOfWork()
        {
            context = new LabContext();

        }
        public static UnitOfWork GetInstance()
        {
            if( instance == null )
            {
                instance = new UnitOfWork();
            }
            return instance;
        }

        public SQLDepartmentEmployeeRepository departmentEmployeeRepository
        {

            get
            {

                if (UnitOfWork.deprtmEmplRep == null)
                {
                    UnitOfWork.deprtmEmplRep = new SQLDepartmentEmployeeRepository(context);
                }
                return deprtmEmplRep;
            }
        }
        public SQLDepartmentRepository departmentRepository
        {

            get
            {

                if (UnitOfWork.departRep == null)
                {
                    UnitOfWork.departRep = new SQLDepartmentRepository(context);
                }
                return departRep;
            }
        }
        public SQLEmployeeRepository  employeeRepository
        {

            get
            {

                if (UnitOfWork.emplRep == null)
                {
                    UnitOfWork.emplRep = new SQLEmployeeRepository(context);
                }
                return emplRep;
            }
        }
        public SQLTaskRepository taskRepository
        {

            get
            {

                if (UnitOfWork.taskRep == null)
                {
                    UnitOfWork.taskRep = new SQLTaskRepository(context);
                }
                return taskRep;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!UnitOfWork.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            UnitOfWork.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
