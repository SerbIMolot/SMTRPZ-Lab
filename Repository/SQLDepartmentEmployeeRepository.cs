using SMTRPZ_IT_company.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SMTRPZ_IT_company.Repository
{
    public class SQLDepartmentEmployeeRepository : IRepository<DepartmentEmployee>
    {
        private LabContext db;

        public SQLDepartmentEmployeeRepository(LabContext context)
        {
            this.db = context;
        }

        public IEnumerable<DepartmentEmployee> GetList()
        {
            return db.DepartmentsEmployees;
        }

        public DepartmentEmployee GetById(int id)
        {
            return db.DepartmentsEmployees.Find(id);
        }
        public DepartmentEmployee GetByIdDetached(int id)
        {
            var depEmpl = db.DepartmentsEmployees.Find(id);
            db.Entry(depEmpl).State = EntityState.Detached;
            return depEmpl;
        }
        public DepartmentEmployee GetByEmployee(Employee employee)
        {
            return db.DepartmentsEmployees.Include( de => de.employee ).SingleOrDefault( e => e.employeeId == employee.employeeId );
        }
        public List< DepartmentEmployee> GetByDepartment(Department department)
        {
            return db.DepartmentsEmployees.Where( d => d.departmentId == department.departmentId ).ToList();
        }

        public int EmployeesInDepartment(Department department)
        {
            return db.DepartmentsEmployees.Count(d => d.departmentId == department.departmentId);
        }

        public void Create(DepartmentEmployee deprEmpl)
        {
            if( deprEmpl == null )
            {
                return;

            }

            deprEmpl.department.numOfEmployees += 1;
            db.Entry(deprEmpl.department).State = EntityState.Modified;

            db.DepartmentsEmployees.Add(deprEmpl);
        }

        public void Update(DepartmentEmployee deprEmpl)
        {
            db.Entry(deprEmpl).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DepartmentEmployee deprEmpl = db.DepartmentsEmployees.Find(id);
            if (deprEmpl != null)
                db.DepartmentsEmployees.Remove(deprEmpl);
        }
        public void Delete( DepartmentEmployee deprEmpl )
        {
            DepartmentEmployee depEmpl = db.DepartmentsEmployees.SingleOrDefault(de => de.departmentId == deprEmpl.departmentId
                                                                        && de.employeeId == deprEmpl.employeeId);
            if (depEmpl != null)
                db.DepartmentsEmployees.Remove(depEmpl);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}