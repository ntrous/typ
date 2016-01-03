using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TradeYourPhone.Core.DTO;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Web.ActionResults;

namespace TradeYourPhone.Web.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        private IConfigurationService configService;

        public ConfigurationController(IConfigurationService configService)
        {
            this.configService = configService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetConfigurationData()
        {
            var configItems = configService.GetAllConfigItems();
            List<ConfigurationDTO> configData = new List<ConfigurationDTO>();
            foreach (var item in configItems)
            {
                ConfigurationDTO config = new ConfigurationDTO();
                config.MapFromObj(item);
                configData.Add(config);
            }

            var result = new JsonNetResult
            {
                SerializerSettings = { DateFormatString = "yyyy-MM-dd" },
                Data = configData
            };

            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveConfigurationData(IList<ConfigurationDTO> configData)
        {
            IEnumerable<Configuration> configList = configService.GetAllConfigItems();
            foreach (var item in configData)
            {
                Configuration config = configList.FirstOrDefault(c => c.Key == item.Key);
                item.MapToObj(config);
            }

            configService.SaveConfigItems(configList.ToList());

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}