using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTRPZ_IT_company.BLL.Services
{
    public interface IManagmentService<T> : IDisposable
        where T : class
    {
        void Add(T VM);
        void Delete(T VM);
        T GetById(int? id);
        IEnumerable<T> GetAll();
        void Dispose();
    }
}
