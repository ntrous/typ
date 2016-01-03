using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.Services.Interface
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Gets all Configuration Items
        /// </summary>
        /// <returns></returns>
        IEnumerable<Configuration> GetAllConfigItems();

        /// <summary>
        /// Gets all Configuration Items
        /// </summary>
        /// <returns></returns>
        bool SaveConfigItems(IList<Configuration> configItems);

        /// <summary>
        /// Gets Configuration Value based on Key Param
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);

        /// <summary>
        /// Sets the value of the given key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        void SetValue(string key, string value);
    }
}
