using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(PhoneModelMD))]
    partial class PhoneModel
    {
        public string PrimaryImageString
        {
            get
            {
                if (PrimaryImage != null)
                {
                    return Convert.ToBase64String(PrimaryImage);
                }
                return string.Empty;
            }
        }
    }

    public class PhoneModelMD
    {

    }
}