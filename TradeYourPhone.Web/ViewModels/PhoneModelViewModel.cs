using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Web.ViewModels
{
    public class PhoneModelViewModel
    {
        public PhoneModelDTO Model { get; set; }
        public IList<PhoneMakeDTO> PhoneMakes { get; set; }

        public void MapPhoneMakes(IList<PhoneMake> phoneMakes)
        {
            PhoneMakes = new List<PhoneMakeDTO>();
            foreach(var make in phoneMakes)
            {
                PhoneMakeDTO phoneMake = new PhoneMakeDTO();
                phoneMake.Map(make);
                PhoneMakes.Add(phoneMake);
            }
        }
    }
}