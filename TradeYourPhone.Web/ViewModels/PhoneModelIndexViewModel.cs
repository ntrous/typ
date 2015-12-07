using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.DTO;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Web.ViewModels
{
    public class PhoneModelIndexViewModel
    {
        public IList<PhoneModelDTO> PhoneModels { get; set; }

        public void MapPhoneModels(IList<PhoneModel> phoneModels)
        {
            PhoneModels = new List<PhoneModelDTO>();
            foreach(var model in phoneModels)
            {
                PhoneModelDTO phoneModelDTO = new PhoneModelDTO();
                phoneModelDTO.Map(model);
                PhoneModels.Add(phoneModelDTO);
            }
        }
    }
}
