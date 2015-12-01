using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext context;
        private IGenericRepository<PhoneMake> phoneMakeRepository;
        private IGenericRepository<PhoneModel> phoneModelRepository;
        private IGenericRepository<PhoneCondition> phoneConditionRepository;
        private IGenericRepository<PhoneConditionPrice> phoneConditionPriceRepository;
        private IGenericRepository<PaymentType> paymentTypeRepository;
        private IGenericRepository<QuoteStatus> quoteStatusRepository;
        private IQuoteRepository quoteRepository;
        private IGenericRepository<State> stateRepository;
        private IGenericRepository<Country> countryRepository;
        private IPhoneRepository phoneRepository;
        private IGenericRepository<PhoneStatu> phoneStatusRepository;
        private IGenericRepository<Customer> customerRepository;
        private IGenericRepository<Address> addressRepository;
        private IGenericRepository<PaymentDetail> paymentDetailRepository;
        private IGenericRepository<PostageMethod> postageMethodRepository;
        private IGenericRepository<QuoteStatusHistory> quoteStatusHistoryRepository;
        private IGenericRepository<PhoneStatusHistory> phoneStatusHistoryRepository;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public IGenericRepository<PhoneMake> PhoneMakeRepository
        {
            get
            {
                if (this.phoneMakeRepository == null)
                {
                    this.phoneMakeRepository = new GenericRepository<PhoneMake>(context);
                }
                return phoneMakeRepository;
            }
            set
            {
                this.phoneMakeRepository = value;
            }
        }

        public IGenericRepository<PhoneModel> PhoneModelRepository
        {
            get
            {
                if (this.phoneModelRepository == null)
                {
                    this.phoneModelRepository = new GenericRepository<PhoneModel>(context);
                }
                return phoneModelRepository;
            }
            set
            {
                this.phoneModelRepository = value;
            }
        }

        public IGenericRepository<PhoneCondition> PhoneConditionRepository
        {
            get
            {
                if (this.phoneConditionRepository == null)
                {
                    this.phoneConditionRepository = new GenericRepository<PhoneCondition>(context);
                }
                return phoneConditionRepository;
            }
            set
            {
                this.phoneConditionRepository = value;
            }
        }

        public IGenericRepository<PhoneConditionPrice> PhoneConditionPriceRepository
        {
            get
            {
                if (this.phoneConditionPriceRepository == null)
                {
                    this.phoneConditionPriceRepository = new GenericRepository<PhoneConditionPrice>(context);
                }
                return phoneConditionPriceRepository;
            }
            set
            {
                this.phoneConditionPriceRepository = value;
            }
        }

        public IGenericRepository<PaymentType> PaymentTypeRepository
        {
            get
            {
                if (this.paymentTypeRepository == null)
                {
                    this.paymentTypeRepository = new GenericRepository<PaymentType>(context);
                }
                return paymentTypeRepository;
            }
            set
            {
                this.paymentTypeRepository = value;
            }
        }

        public IGenericRepository<QuoteStatus> QuoteStatusRepository
        {
            get
            {
                if (this.quoteStatusRepository == null)
                {
                    this.quoteStatusRepository = new GenericRepository<QuoteStatus>(context);
                }
                return quoteStatusRepository;
            }
            set
            {
                this.quoteStatusRepository = value;
            }
        }

        public IQuoteRepository QuoteRepository
        {
            get
            {
                if (this.quoteRepository == null)
                {
                    this.quoteRepository = new QuoteRepository(context);
                }
                return quoteRepository;
            }
            set
            {
                this.quoteRepository = value;
            }
        }

        public IGenericRepository<State> StateRepository
        {
            get
            {
                if (this.stateRepository == null)
                {
                    this.stateRepository = new GenericRepository<State>(context);
                }
                return stateRepository;
            }
            set
            {
                this.stateRepository = value;
            }
        }

        public IGenericRepository<Country> CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                {
                    this.countryRepository = new GenericRepository<Country>(context);
                }
                return countryRepository;
            }
            set
            {
                this.countryRepository = value;
            }
        }

        public IPhoneRepository PhoneRepository
        {
            get
            {
                if (this.phoneRepository == null)
                {
                    this.phoneRepository = new PhoneRepository(context);
                }
                return phoneRepository;
            }
            set
            {
                this.phoneRepository = value;
            }
        }

        public IGenericRepository<PhoneStatu> PhoneStatusRepository
        {
            get
            {
                if (this.phoneStatusRepository == null)
                {
                    this.phoneStatusRepository = new GenericRepository<PhoneStatu>(context);
                }
                return phoneStatusRepository;
            }
            set
            {
                this.phoneStatusRepository = value;
            }
        }

        public IGenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customer>(context);
                }
                return customerRepository;
            }
            set
            {
                this.customerRepository = value;
            }
        }

        public IGenericRepository<Address> AddressRepository
        {
            get
            {
                if (this.addressRepository == null)
                {
                    this.addressRepository = new GenericRepository<Address>(context);
                }
                return addressRepository;
            }
            set
            {
                this.addressRepository = value;
            }
        }

        public IGenericRepository<PaymentDetail> PaymentDetailRepository
        {
            get
            {
                if (this.paymentDetailRepository == null)
                {
                    this.paymentDetailRepository = new GenericRepository<PaymentDetail>(context);
                }
                return paymentDetailRepository;
            }
            set
            {
                this.paymentDetailRepository = value;
            }
        }

        public IGenericRepository<PostageMethod> PostageMethodRepository
        {
            get
            {
                if (this.postageMethodRepository == null)
                {
                    this.postageMethodRepository = new GenericRepository<PostageMethod>(context);
                }
                return postageMethodRepository;
            }
            set
            {
                this.postageMethodRepository = value;
            }
        }

        public IGenericRepository<QuoteStatusHistory> QuoteStatusHistoryRepository
        {
            get
            {
                if (this.quoteStatusHistoryRepository == null)
                {
                    this.quoteStatusHistoryRepository = new GenericRepository<QuoteStatusHistory>(context);
                }
                return quoteStatusHistoryRepository;
            }
            set
            {
                this.quoteStatusHistoryRepository = value;
            }
        }

        public IGenericRepository<PhoneStatusHistory> PhoneStatusHistoryRepository
        {
            get
            {
                if (this.phoneStatusHistoryRepository == null)
                {
                    this.phoneStatusHistoryRepository = new GenericRepository<PhoneStatusHistory>(context);
                }
                return phoneStatusHistoryRepository;
            }
            set
            {
                this.phoneStatusHistoryRepository = value;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
