using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMTRPZ_IT_company.Repository
{
    interface IRepository<T> : IDisposable
            where T : class
    {
        IEnumerable<T> GetList(); 
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}