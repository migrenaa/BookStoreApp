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
    public class StoreController : BaseController

    {  private readonly IStoreService service;
        private readonly IBooksInStoreService serviceBooksInStore;

        public StoreController(IStoreService service, IBooksInStoreService serviceBIS)
        {
            this.service = service;
            this.serviceBooksInStore = serviceBIS;
        }

        // GET api/store
        public IEnumerable<StoreViewModel> Get()
        { 

            return service.GetAll();
        }

        //GET api/store/1
        [Route("api/store/{id}")]
        public IHttpActionResult Get(int id)
        {
            var store = service.Get(id);
            if(store == null)
            {
                return NotFound();
            }
            return Ok(store);   
        }
     
        //POST api/store
        public IHttpActionResult Post(StoreViewModel store)
        {
            if(store == null && !ModelState.IsValid)
            {
                return BadRequest();
            }
            service.Add(store);
            return Ok(store);
        }

        //DELETE api/store/1
        [Route("api/store/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var deleted = service.Get(id);
            if (deleted == null)
            {
                return NotFound();
            }
            service.Remove(id);
            return Ok(deleted);
        }

        //PUT api/store/1
        [Route("api/store/{id}")]
        public IHttpActionResult Put(int id, StoreViewModel store)
        {
            var s = service.Get(id);  
            if(s == null)
            {
                return NotFound();
            }

            if(store == null && !ModelState.IsValid)
            {
                return BadRequest();
            }
            service.Update(id, store);
            return Ok(store);
        }

        [HttpGet]
        [Route("api/store/books/{id}")]
        //GET api/store/{id}
        public IEnumerable<BooksViewModel> GetAllBooksInStore(int id)
        {
            var store = service.Get(id);
            if (store != null)
            {
                var books = serviceBooksInStore.GetAllBooksInStore(id);
                return books;
            }
            return null;
        }

        [HttpPost]
        [Route("api/store/book")]
        //POST api/storebooks/
        public IHttpActionResult AddBookInStore(BooksInStoreRequestModel item)
        {
            if (item == null && !ModelState.IsValid)
            {
                return BadRequest();
            }
            var added = serviceBooksInStore.AddBookToStore(item);
            return Ok(added);

        }

        //DELETE api/book/storebooks/1
        [HttpDelete]
        [Route("api/store/books/{id}")]
        public IHttpActionResult DeleteBookStoreRelationship([FromUri] int id)
        {
            var toRemove = serviceBooksInStore.RemoveBookFromStore(id);
            if (toRemove == null)
            {
                return NotFound();
            }

            return Ok(toRemove);
        }

    }
}