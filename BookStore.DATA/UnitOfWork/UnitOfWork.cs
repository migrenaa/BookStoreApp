using BookStore.DATA.ADO;
using BookStore.DATA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DATA.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private BookStoreEntities Context = new BookStoreEntities();
        public GenericRepository<Book> books;
        public GenericRepository<Store> stores;
        public GenericRepository<BooksInStore> booksInStore;


        private bool disposed = false;


        public GenericRepository<Book> Books
        {
            get
            {
                if (this.books == null)
                {
                    books = new GenericRepository<Book>(this.Context);
                }
                return books;
            }
        }
        public GenericRepository<Store> Stores
        {
            get
            {
                if (this.stores == null)
                {
                    stores = new GenericRepository<Store>(this.Context);
                }
                return stores;
            }
        }
        public GenericRepository<BooksInStore> BooksInStore
        {
            get
            {
                if (this.booksInStore == null)
                {
                    booksInStore = new GenericRepository<BooksInStore>(this.Context);
                }
                return booksInStore;
            }
        }

        public void Save()
        {
            Context.SaveChanges();
        } 

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
