using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T,ID>
    {
        void Add(T entity);
        IEnumerable<T> Get();
        T Get(ID id);
        void Edit(T entity);
        void Delete(ID id);

    }
}
