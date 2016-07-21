using BookStore.DATA;
using BookStore.DATA.ADO;
using BookStore.DATA.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Service.Mapping;

namespace BookStore.Service
{
    public class BooksService : IBookService
    {
        private readonly IUnitOfWork data;
       
           


        public BooksService(IUnitOfWork data)
        {
            Mapper.CreateMap<Book, BooksViewModel>();
            Mapper.CreateMap<BooksRequestModel, Book>();
            this.data = data;
        }

        public IEnumerable<BooksViewModel> GetAll()
        {
             return data.Books.GetAll().AsQueryable().To<BooksViewModel>();
        }

        public BooksViewModel Get(int id)
        {
            var book = data.Books.GetByID(id);
            if (book != null)
            {
                return Mapper.Map<BooksViewModel>(book);
            }
            return null;
        }

        public BooksViewModel Add(BooksRequestModel book)
        {
            if (book != null)
            {
                var b = Mapper.Map<Book>(book);
                data.Books.Insert(b);
                data.Save();
                return Mapper.Map<BooksViewModel>(b);
                
            }
            return null;
        }

        public BooksViewModel Update(int id, BooksRequestModel book)
        {
            var updated = data.Books.GetByID(id);
            
            if (book != null && updated != null)
            {
                updated.Name = book.Name;
                updated.Author = book.Author;
                updated.Price = book.Price;
                data.Save();
                return Mapper.Map<BooksViewModel>(updated);
            }
            return null;
        }

        public BooksViewModel Remove(int id)
        {
            var toRemove = data.Books.GetByID(id);

            if (toRemove != null)
            {
                var allConnections = data.BooksInStore.GetAll().AsEnumerable();
                var booksToRemove = allConnections.Where<BooksInStore>(b => b.BookId == id);
                foreach (var book in booksToRemove)
                {

                    var connectionToRemove = data.BooksInStore.GetByID(book.Id);
                    data.BooksInStore.Remove(connectionToRemove);

                }
                
                data.Books.Remove(toRemove);
                data.Save();
                return Mapper.Map<BooksViewModel>(toRemove);
            }
            return null;
        }
    }
}
