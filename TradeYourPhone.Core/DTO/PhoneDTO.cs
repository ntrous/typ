﻿using System;
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
        public string IMEI { get; set; }
        public Nullable<decimal> SaleAmount { get; set; }
        public Nullable<decimal> PurchaseAmount { get; set; }
        public List<PhoneStatusHistoryDTO> PhoneStatusHistories { get; set; }

        public void MapToDTO(Phone phone)
        {
            Id = phone.Id;
            QuoteId = phone.QuoteId;
            PhoneMakeId = phone.PhoneMakeId;
            PhoneModelId = phone.PhoneModelId;
            PhoneConditionId = phone.PhoneConditionId;
            PhoneStatusId = phone.PhoneStatusId;
            IMEI = phone.IMEI;
            SaleAmount = phone.SaleAmount;
            PurchaseAmount = phone.PurchaseAmount;

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