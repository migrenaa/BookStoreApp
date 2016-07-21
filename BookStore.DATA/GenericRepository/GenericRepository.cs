using BookStore.DATA.ADO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DATA.Interfaces
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private BookStoreEntities context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(BookStoreEntities context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }


        public TEntity GetByID(int id)
        {
            return dbSet.Find(id);            
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if(filter != null)
            {
                query = query.Where<TEntity>(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }


            if (orderBy != null)
            {
                query = orderBy(query).AsQueryable<TEntity>();
            }

            return query;

        }

        public void Insert(TEntity t)
        {
            dbSet.Add(t);
        }

        public void Remove(TEntity item)
        {
                dbSet.Remove(item);
        }


        public void Update(int id, TEntity item)
        {
            dbSet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }

    }
}
