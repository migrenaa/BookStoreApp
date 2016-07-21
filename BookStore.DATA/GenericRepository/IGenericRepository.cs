using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DATA
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        T GetByID(int id);
        void Insert(T item);
        void Update(int id, T item);
        T Remove(int id);
        void Save();

    }
}
