using BookStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interfaces
{
    public interface IBooksInStoreService
    {
        BooksInStoresViewModel Get(int id);
        IEnumerable<BooksInStoresViewModel> GetAll();
        BooksInStoresViewModel Add(BooksInStoreRequestModel item);
        BooksInStoresViewModel Update(int id, BooksInStoreRequestModel item);
        BooksInStoresViewModel Remove(int id);

        BooksInStoresViewModel AddBookToStore(BooksInStoreRequestModel item);
        BooksInStoresViewModel RemoveBookFromStore(int id);
        
        IEnumerable<BooksViewModel> GetAllBooksInStore(int id);
        IEnumerable<StoreViewModel> GetAllStoresWithBook(int id);
    }
}
