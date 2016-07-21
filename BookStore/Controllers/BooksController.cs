using BookStore.DATA.UnitOfWork;
using BookStore.Service;
using BookStore.Service.Interfaces;
using BookStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BookStore.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IBookService service;
        private readonly IBooksInStoreService serviceConnection;

        public BooksController(IBookService service, IBooksInStoreService serviceBooksInStore)
        {
            this.serviceConnection = serviceBooksInStore;
            this.service = service;
        }

        // GET api/book
        public IEnumerable<BooksViewModel> Get()
        {
            return service.GetAll();
        }

        //GET api/book/1
        public IHttpActionResult Get(int id)
        {
            var book = service.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        //POST api/book
        public IHttpActionResult Post(BooksRequestModel book)
        {
            if (book == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(service.Add(book));
        }



        //DELETE api/book/1
        public IHttpActionResult Delete(int id)
        {
            var deleted = service.Get(id);
            if (deleted != null)
            {
                service.Remove(id);
                
               return Ok(new BooksViewModel { Name = deleted.Name, Price = deleted.Price });
            }
            return NotFound();

        }

        //PUT api/book/1
        public IHttpActionResult Put(int id, BooksRequestModel book)
        {
            var toUpdate = service.Get(id);
            if (toUpdate == null)
            {
                return NotFound();
            }

            if (book == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var updated = service.Update(id, book);
            return Ok(updated);
        }


        [HttpGet]
        [Route("api/books/store/{id}")]
        //GET api/store/{id}
        public IEnumerable<StoreViewModel> GetAllStoresWithBook(int id)
        {
            var book = service.Get(id);
            if (book != null)
            {
                var stores = serviceConnection.GetAllStoresWithBook(id);
                return stores;
            }
            return null;
        }
    }
}