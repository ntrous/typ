using TradeYourPhone.Test.DummyRepositories;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Repositories.Implementation;
using TradeYourPhone.Core.Repositories.Interface;
using TradeYourPhone.Core.Services.Implementation;
using TradeYourPhone.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Test.Models;
using TradeYourPhone.Test.TradeYourPhone.Core.Services.MockServices;

namespace TradeYourPhone.Test.SetupData
{
    public class CreateMockData
    {
        private IUnitOfWork _unitOfWork;

        public CreateMockData()
        {
            _unitOfWork = new UnitOfWork(new NavexaMobile_UnitTestEntities());
        }

        public IPhoneService GetPhoneService()
        {
            return new PhoneService(unitOfWork);
        }

        public IQuoteService GetQuoteService()
        {
            return new QuoteService(unitOfWork, GetPhoneService(), GetEmailService());
        }

        public IEmailService GetEmailService()
        {
            return new EmailServiceMock();
        }

        public IUnitOfWork unitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }
    }
}
