using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.Repositories.Interface
{
    public interface IPhoneRepository : IGenericRepository<Phone>
    {
        void Insert(Phone entity, string userId);
        void Update(Phone entityToUpdate, string userId);
        int GetPhoneStatusId(int quoteId);
        Phone GetNewPhoneObject();
    }
}
