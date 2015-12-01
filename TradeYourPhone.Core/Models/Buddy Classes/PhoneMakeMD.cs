using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(PhoneMakeMD))]
    partial class PhoneMake
    {

        public void UpdateFromDTO(PhoneMakeDTO phoneMakeDTO)
        {
            ID = phoneMakeDTO.ID;
            MakeName = phoneMakeDTO.Name;
        }
        public class PhoneMakeMD
        {

        }
    }
}