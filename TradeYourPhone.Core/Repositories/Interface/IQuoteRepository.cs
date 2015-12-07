using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.Repositories.Interface
{
    public interface IQuoteRepository : IGenericRepository<Quote>
    {
        void Insert(Quote entity, string userId);
        void Update(Quote entityToUpdate, string userId);
        int GetQuoteStatusId(int quoteId);
        bool DoesQuoteExist(string quoteReferenceId);
    }
}
