using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public interface IBookService
    {
        IEnumerable<BooksViewModel> GetAll();
        BooksViewModel Get(int id);
        BooksViewModel Add(BooksRequestModel book);
        BooksViewModel Update(int id, BooksRequestModel book);
        BooksViewModel Remove(int id);
    }
}
