using BookStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Service.Models;
using BookStore.DATA.Interfaces;
using BookStore.DATA.UnitOfWork;
using BookStore.DATA.ADO;
using BookStore.Service.Mapping;
using AutoMapper;

namespace BookStore.Service
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork data;

        public StoreService(IUnitOfWork repo)
        {

            Mapper.CreateMap<Store, StoreViewModel>();
            this.data = repo;
        }

        public StoreViewModel Add(StoreViewModel store)
        {
            if(store != null)
            {
                data.Stores.Insert(new Store { Name = store.Name});
                data.Save();
                return store;
            }
            return null;
        }

        public StoreViewModel Get(int id)
        {
            var store = data.Stores.GetByID(id);
            if(store == null)
            {
                return null;
            }
            return Mapper.Map<StoreViewModel>(store);
        }
    

        public IEnumerable<StoreViewModel> GetAll()
        {
         
            return data.Stores.GetAll().AsQueryable().To<StoreViewModel>();
        }

        public StoreViewModel Remove(int id)
        {

            var toRemove = data.Stores.GetByID(id); 
            if(toRemove != null)
            {
                var allConnections = data.BooksInStore.GetAll().AsEnumerable();
                var storesToRemove = allConnections.Where<BooksInStore>(s => s.StoreId == id);
                foreach (var store in storesToRemove)
                {

                    var connectionToRemove = data.BooksInStore.GetByID(store.Id);
                    data.BooksInStore.Remove(connectionToRemove);

                }
                data.Stores.Remove(toRemove);
                data.Save();
                return Mapper.Map<StoreViewModel>(toRemove);
            }
            return null;
        }

        public StoreViewModel Update(int id, StoreViewModel store)
        {
            var a = data.Stores.GetByID(id);
            if (store != null && data.Stores.GetByID(id) != null)
            {
                a.Name = store.Name;
                data.Save();
                return store;
            }
            return null;
        }
    }
}
