using SMTRPZ_IT_company.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SMTRPZ_IT_company.Repository
{
    public class SQLDepartmentRepository : IRepository<Department>
    {
        private LabContext db;

        public SQLDepartmentRepository(LabContext context)
        {
            this.db = context;
        }

        public IEnumerable<Department> GetList()
        {
            return db.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return db.Departments.Find(id);
        }
        public Department GetByIdDetached(int id)
        {
            var dep = db.Departments.Find(id);
            db.Entry(dep).State = EntityState.Detached;
            return dep;
        }
        public Department GetByName( string name )
        {
            return db.Departments.FirstOrDefault(a => a.departmentName == name);
        }

        public void Create(Department department)
        {
            db.Departments.Add(department);
        }

        public void Update(Department department)
        {
            db.Entry(department).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Department department = db.Departments.Find(id);
            if (department != null)
                db.Departments.Remove(department);
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