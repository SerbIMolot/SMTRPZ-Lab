using SMTRPZ_IT_company.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SMTRPZ_IT_company.Repository
{
    public class SQLEmployeeRepository : IRepository<Employee>
    {
        private LabContext db;

        public SQLEmployeeRepository(LabContext context)
        {
            this.db = context;
        }

        public IEnumerable<Employee> GetList()
        {
            return db.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return db.Employees.Find(id);
        }
        public Employee GetByIdDetached(int id)
        {
            var empl = db.Employees.Find(id);
            db.Entry(empl).State = EntityState.Detached;
            return empl;
        }
        //public Employee GetByName( string name )
        //{
        //    return db.Employees.Find(id);
        //}

        public void Create(Employee employee)
        {
            db.Employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            var local = db.Employees
                .Local
                .FirstOrDefault(e => e.employeeId == employee.employeeId);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }

            db.Entry(employee).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
                db.Employees.Remove(employee);
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