using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.Repositories.Interface
{
    public interface IConfigurationRepository : IGenericRepository<Configuration>
    {
        string GetValue(string key);
        void SetValue(string key, string value);
    }
}
