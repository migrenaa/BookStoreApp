using BookStore.DATA.ADO;
using BookStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interfaces
{
    public interface IStoreService
    {
        IEnumerable<StoreViewModel> GetAll();
        StoreViewModel Get(int id);
        StoreViewModel Add(StoreViewModel store);
        StoreViewModel Update(int id, StoreViewModel store);
        StoreViewModel Remove(int id);

    }
}
