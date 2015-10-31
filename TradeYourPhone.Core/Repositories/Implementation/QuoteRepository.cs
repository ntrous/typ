using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Repositories.Interface;

namespace TradeYourPhone.Core.Repositories.Implementation
{
    public class QuoteRepository : IGenericRepository<QuoteRepository>
    {
        internal DbContext context;
        internal DbSet<QuoteRepository> dbSet;

        public QuoteRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<QuoteRepository>();
        }

        public virtual IEnumerable<QuoteRepository> Get(
            Expression<Func<QuoteRepository, bool>> filter = null,
            Func<IQueryable<QuoteRepository>, IOrderedQueryable<QuoteRepository>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<QuoteRepository> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual QuoteRepository GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(QuoteRepository entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            QuoteRepository entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(QuoteRepository entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(QuoteRepository entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
