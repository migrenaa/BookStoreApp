using BookStore.DATA.ADO;
using BookStore.DATA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DATA.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Book> Books { get; }
        GenericRepository<Store> Stores { get; }
        GenericRepository<BooksInStore> BooksInStore { get; }
        void Save();
    }
}
