using System;
using System.Data.Entity;
using System.Linq;
using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Repositories.Interface;
using TradeYourPhone.Core.Utilities;

namespace TradeYourPhone.Core.Repositories.Implementation
{
    public class QuoteRepository : GenericRepository<Quote>, IQuoteRepository
    {
        public QuoteRepository(DbContext context) : base(context) { }

        public void Insert(Quote entity, string userId)
        {
            this.dbSet.Add(entity);
            context.SaveChanges();
            
            var quoteStatusHistorydbSet = context.Set<QuoteStatusHistory>();
            QuoteStatusHistory record = quoteStatusHistorydbSet.Create();
            record.QuoteId = entity.ID;
            record.QuoteStatusId = entity.QuoteStatusId;
            record.StatusDate = Util.GetAEST(DateTime.Now);
            record.CreatedBy = userId ?? User.SystemUser.Value;

            quoteStatusHistorydbSet.Add(record);
        }

        public void Update(Quote entityToUpdate, string userId)
        {
            var quoteEntry = context.Entry(entityToUpdate);
            var QuoteStatusProp = quoteEntry.Property("QuoteStatusId");

            if(QuoteStatusProp.IsModified)
            {
                var dbSet = context.Set<QuoteStatusHistory>();
                QuoteStatusHistory record = dbSet.Create();
                record.QuoteId = entityToUpdate.ID;
                record.QuoteStatusId = entityToUpdate.QuoteStatusId;
                record.StatusDate = Util.GetAEST(DateTime.Now);
                record.CreatedBy = userId ?? User.SystemUser.Value;

                dbSet.Add(record);
            }

            quoteEntry.State = EntityState.Modified;
        }

        public int GetQuoteStatusId(int quoteId)
        {
            IQueryable<Quote> query = context.Set<Quote>();
            int currentStatus = query.Where(q => q.ID == quoteId).Select(p => p.QuoteStatusId).First();
            return currentStatus;
        }

        public bool DoesQuoteExist(string quoteReferenceId)
        {
            IQueryable<Quote> query = context.Set<Quote>();
            bool exists = query.Any(q => q.QuoteReferenceId == quoteReferenceId);
            return exists;
        }
    }
}
