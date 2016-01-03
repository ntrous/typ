using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Repositories.Interface;
using TradeYourPhone.Core.Services.Interface;

namespace TradeYourPhone.Core.Services.Implementation
{
    public class ConfigurationService : IConfigurationService
    {
        private IUnitOfWork unitOfWork;

        public ConfigurationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all Configuration Items
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Configuration> GetAllConfigItems()
        {
            return unitOfWork.ConfigurationRepository.Get();
        }

        /// <summary>
        /// Saves all config items passed in parameter
        /// </summary>
        /// <param name="configItems"></param>
        /// <returns></returns>
        public bool SaveConfigItems(IList<Configuration> configItems)
        {
            try
            {
                foreach (var item in configItems)
                {
                    unitOfWork.ConfigurationRepository.Update(item);
                }

                unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets Configuration Value based on Key Param
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            string value = unitOfWork.ConfigurationRepository.GetValue(key);
            return value;
        }

        /// <summary>
        /// Sets the value of the given key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetValue(string key, string value)
        {
            unitOfWork.ConfigurationRepository.SetValue(key, value);
        }
    }
}
