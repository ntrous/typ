using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class ConfigurationDTO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string UserCreatedBy { get; set; }
        public string UserLastModifiedBy { get; set; }

        /// <summary>
        /// Maps contents of DTO into the provided object
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public Configuration MapToObj(Configuration config)
        {
            config.Value = Value;
            config.Description = Description;
            return config;
        }

        /// <summary>
        /// Maps Object into the DTO
        /// </summary>
        /// <param name="config"></param>
        public void MapFromObj(Configuration config)
        {
            Id = config.Id;
            Key = config.Key;
            Value = config.Value;
            Description = config.Description;
            CreatedDate = config.CreatedDate;
            LastModifiedDate = config.LastModifiedDate;
            UserCreatedBy = config.CreatedByUser.UserName;
            UserLastModifiedBy = config.LastModifiedByUser?.UserName;
        }
    }
}
