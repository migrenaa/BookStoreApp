using BookStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Service.Models;
using BookStore.DATA.UnitOfWork;
using BookStore.DATA.ADO;
using AutoMapper;

namespace BookStore.Service
{
    public class BooksInStores : IBooksInStoreService
    {
        UnitOfWork data = new UnitOfWork();

        public BooksInStores(UnitOfWork unitOfWork)
        {

            Mapper.CreateMap<BooksInStores, BooksInStoresViewModel>();
            this.data = unitOfWork;
        }


        public IEnumerable<BooksInStoresViewModel> GetAll()
        {
            List<BooksInStoresViewModel> connectionsVM = new List<BooksInStoresViewModel>();
            var shit = data.BooksInStore.GetAll().ToList();
            foreach (var b in shit)
            {
                connectionsVM.Add(new BooksInStoresViewModel {
                    Book = data.Books.GetByID(b.BookId).Name,
                    Author = data.Books.GetByID(b.BookId).Author,
                    Store = data.Stores.GetByID(b.StoreId).Name,
                });
            }
            return connectionsVM;
        }

        public BooksInStoresViewModel Get(int id)
        {
            return new BooksInStoresViewModel { Book = data.Books.GetByID(id).Name, Author = data.Books.GetByID(id).Author, Store = data.Stores.GetByID(id).Name };
        }

        public BooksInStoresViewModel Add(BooksInStoreRequestModel item)
        {
            if (item != null)
            {
                data.BooksInStore.Insert(new BooksInStore {BookId =  item.BookId, StoreId = item.StoreId});
                data.Save();
                return new BooksInStoresViewModel
                {
                    Book = data.Books.GetByID(item.BookId).Name,
                    Author = data.Books.GetByID(item.BookId).Author,
                    Store = data.Stores.GetByID(item.StoreId).Name,
                };
            }
            return null;
        }

        public BooksInStoresViewModel Update(int id, BooksInStoreRequestModel item)
        {
            var toUpdate = data.BooksInStore.GetByID(id);
            if (toUpdate != null)
            {
                data.BooksInStore.Update(id, toUpdate);
                data.Save();
                return new BooksInStoresViewModel
                {
                    Book = data.Books.GetByID(item.BookId).Name,
                    Author = data.Books.GetByID(item.BookId).Author,
                    Store = data.Stores.GetByID(item.StoreId).Name,
                };
            }
            return null;
        }

        public BooksInStoresViewModel Remove(int id)
        {
            var toRemove = data.BooksInStore.GetByID(id);
            if (toRemove != null)
            {
                data.BooksInStore.Remove(toRemove);
                data.Save();
                return new BooksInStoresViewModel
                {
                    Book = data.Books.GetByID(id).Name,
                    Author = data.Books.GetByID(id).Author,
                    Store = data.Stores.GetByID(id).Name,
                };
            }
            return null;
        }

        public BooksInStoresViewModel AddBookToStore(BooksInStoreRequestModel item)
        {
            if (item == null)
            {
                return null;
            }

            var book = data.Books.GetByID(item.BookId);
            var store = data.Stores.GetByID(item.StoreId);

            data.BooksInStore.Insert(new DATA.ADO.BooksInStore { BookId = book.Id, StoreId = store.Id, });
            data.Save();

            return new BooksInStoresViewModel
            {
                Book = book.Name,
                Author = book.Author,
                Store = store.Name,
            };
        }

        public IEnumerable<BooksViewModel> GetAllBooksInStore(int storeID)
        {
            IEnumerable<BooksInStore> booksInTheStore = data.BooksInStore.GetAll().Where<BooksInStore>(book => book.StoreId == storeID);
             List<Book> books = new List<Book>();
            
            foreach (var b in booksInTheStore)
            {
                var x = data.Books.GetByID(b.BookId);

                if(x != null)
                {
                    books.Add(x);

                }
            }
            List<BooksViewModel> toReturn = new List<BooksViewModel>();
            foreach (var book in books)
            {
                toReturn.Add(new BooksViewModel { Name = book.Name, Price = book.Price, });
            }
            return toReturn;
        }

        public IEnumerable<StoreViewModel> GetAllStoresWithBook(int id)
        {
            IEnumerable<BooksInStore> storesWithBooks = data.BooksInStore.GetAll().Where<BooksInStore>(store => store.BookId == id);
            List<Store> stores = new List<Store>();

            foreach (var s in storesWithBooks)
            {
                var x = data.Stores.GetByID(s.StoreId);

                if(x!= null)
                {
                    stores.Add(x);
                }
            }
            List<StoreViewModel> toReturn = new List<StoreViewModel>();
            foreach (var store in stores)
            {
                toReturn.Add(new StoreViewModel { Name = store.Name, });
            }

            return toReturn;
        }

        
        public BooksInStoresViewModel RemoveBookFromStore(int id)
        {

            if (data.BooksInStore == null)
            {
                return null;
            }
            var connection = data.booksInStore.GetByID(id);

            if (connection == null)
            {
                return null;
            }

            var book = data.Books.GetByID(id);
            var store = data.Stores.GetByID(id);
            

            data.booksInStore.Remove(connection);
            data.Save();

            return new BooksInStoresViewModel
            {
                Book = book.Name,
                Author = book.Author,
                Store = store.Name,
            };
        }
        
    }
}
