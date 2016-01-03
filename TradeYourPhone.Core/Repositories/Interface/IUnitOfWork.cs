using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.Repositories.Interface
{
    public interface IUnitOfWork
    {
        void Save();
        IGenericRepository<PhoneMake> PhoneMakeRepository { get; set; }
        IGenericRepository<PhoneModel> PhoneModelRepository { get; set; }
        IGenericRepository<PhoneCondition> PhoneConditionRepository { get; set; }
        IGenericRepository<PhoneConditionPrice> PhoneConditionPriceRepository { get; set; }
        IGenericRepository<PaymentType> PaymentTypeRepository { get; set; }
        IGenericRepository<QuoteStatus> QuoteStatusRepository { get; set; }
        IQuoteRepository QuoteRepository { get; set; }
        IGenericRepository<State> StateRepository { get; set; }
        IGenericRepository<Country> CountryRepository { get; set; }
        IPhoneRepository PhoneRepository { get; set; }
        IGenericRepository<PhoneStatu> PhoneStatusRepository { get; set; }
        IGenericRepository<Customer> CustomerRepository { get; set; }
        IGenericRepository<Address> AddressRepository { get; set; }
        IGenericRepository<PaymentDetail> PaymentDetailRepository { get; set; }
        IGenericRepository<PostageMethod> PostageMethodRepository { get; set; }
        IGenericRepository<QuoteStatusHistory> QuoteStatusHistoryRepository { get; set; }
        IGenericRepository<PhoneStatusHistory> PhoneStatusHistoryRepository { get; set; }
        IConfigurationRepository ConfigurationRepository { get; set; }
    }
}
