using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Repositories.Interface;

namespace TradeYourPhone.Core.Repositories.Implementation
{
    public class ConfigurationRepository : GenericRepository<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(DbContext context) : base(context) { }

        public string GetValue(string key)
        {
            IQueryable<Configuration> query = context.Set<Configuration>();
            string value = query.Where(c => c.Key == key).Select(c => c.Value).First();
            return value;
        }

        public void SetValue(string key, string value)
        {
            var configEntry = Get(c => c.Key == key).FirstOrDefault();
            configEntry.Value = value;
            Update(configEntry);
        }
    }
}
