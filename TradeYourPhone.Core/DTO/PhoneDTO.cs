using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class PhoneDTO
    {
        public int Id { get; set; }
        public Nullable<int> QuoteId { get; set; }
        public int PhoneMakeId { get; set; }
        public int PhoneModelId { get; set; }
        public int PhoneConditionId { get; set; }
        public int PhoneStatusId { get; set; }
        public string PhoneMakeName { get; set; }
        public string PhoneModelName { get; set; }
        public string PhoneCondition { get; set; }
        public string PrimaryImageString { get; set; }
        public string IMEI { get; set; }
        public string SaleAmount { get; set; }
        public string PurchaseAmount { get; set; }
        public List<PhoneStatusHistoryDTO> PhoneStatusHistories { get; set; }

        public void MapToDTO(Phone phone)
        {
            Id = phone.Id;
            QuoteId = phone.QuoteId;
            PhoneMakeId = phone.PhoneMakeId;
            PhoneModelId = phone.PhoneModelId;
            PhoneConditionId = phone.PhoneConditionId;
            PhoneStatusId = phone.PhoneStatusId;
            PhoneMakeName = phone.PhoneMake.MakeName;
            PhoneModelName = phone.PhoneModel.ModelName;
            PhoneCondition = phone.PhoneCondition.Condition;
            PrimaryImageString = phone.PhoneModel.PrimaryImageString;
            IMEI = phone.IMEI;
            SaleAmount = phone.SaleAmount.HasValue ? phone.SaleAmount.Value.ToString("F") : string.Empty;
            PurchaseAmount = phone.PurchaseAmount.Value.ToString("F");

            PhoneStatusHistories = new List<PhoneStatusHistoryDTO>();
            foreach (var statusHistory in phone.PhoneStatusHistories.OrderBy(psh => psh.StatusDate))
            {
                PhoneStatusHistoryDTO statusHistoryDTO = new PhoneStatusHistoryDTO();
                statusHistoryDTO.Map(statusHistory);
                PhoneStatusHistories.Add(statusHistoryDTO);
            }
        }
    }
}
