using SMTRPZ_IT_company.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SMTRPZ_IT_company.Repository
{
    public class SQLTaskRepository : IRepository<EmplTask>
    {
        private LabContext db;

        public SQLTaskRepository(LabContext context)
        {
            this.db = context;
        }

        public IEnumerable<EmplTask> GetList()
        {
            return db.Tasks.ToList();
        }
        public EmplTask GetByIdDetached(int id)
        {
            var task = db.Tasks.Find(id);
            db.Entry(task).State = EntityState.Detached;
            return task;
        }
        public EmplTask GetById(int id)
        {
            return db.Tasks.Find(id);
        }
        public IEnumerable<EmplTask> GetByEmployee( Employee employee )
        {
            return db.Tasks.Where(a => a.employeeId == employee.employeeId).ToList();
        }

        public void Create(EmplTask task)
        {
            db.Tasks.Add(task);
        }

        public void Update(EmplTask task)
        {
            var local = db.Tasks
            .Local
            .FirstOrDefault(e => e.taskId == task.taskId);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }

            db.Entry(task).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            EmplTask task = db.Tasks.Find(id);
            if (task != null)
                db.Tasks.Remove(task);
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