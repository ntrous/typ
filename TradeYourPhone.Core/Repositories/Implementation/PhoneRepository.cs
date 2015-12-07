using System;
using System.Data.Entity;
using System.Linq;
using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Repositories.Interface;
using TradeYourPhone.Core.Utilities;

namespace TradeYourPhone.Core.Repositories.Implementation
{
    public class PhoneRepository : GenericRepository<Phone>, IPhoneRepository
    {
        public PhoneRepository(DbContext context) : base(context) { }

        public void Insert(Phone entity, string userId)
        {
            this.dbSet.Add(entity);
            context.SaveChanges();

            var phoneStatusHistorydbSet = context.Set<PhoneStatusHistory>();
            PhoneStatusHistory record = phoneStatusHistorydbSet.Create();
            record.PhoneId = entity.Id;
            record.PhoneStatusId = entity.PhoneStatusId;
            record.StatusDate = Util.GetAEST(DateTime.Now);
            record.CreatedBy = userId ?? User.SystemUser.Value;

            phoneStatusHistorydbSet.Add(record);
        }

        public void Update(Phone entityToUpdate, string userId)
        {
            var phoneEntry = context.Entry(entityToUpdate);
            var PhoneStatusProp = phoneEntry.Property("PhoneStatusId");

            if (PhoneStatusProp.IsModified)
            {
                var dbSet = context.Set<PhoneStatusHistory>();
                PhoneStatusHistory record = dbSet.Create();
                record.PhoneId = entityToUpdate.Id;
                record.PhoneStatusId = entityToUpdate.PhoneStatusId;
                record.StatusDate = Util.GetAEST(DateTime.Now);
                record.CreatedBy = userId ?? User.SystemUser.Value;

                dbSet.Add(record);
            }

            phoneEntry.State = EntityState.Modified;
        }

        public int GetPhoneStatusId(int phoneId)
        {
            IQueryable<Phone> query = context.Set<Phone>();
            int currentStatus = query.Where(p => p.Id == phoneId).Select(p => p.PhoneStatusId).First();
            return currentStatus;
        }

        /// <summary>
        /// Creates a New Phone object off the context so it is tracked by Entity Framework
        /// </summary>
        /// <returns></returns>
        public Phone GetNewPhoneObject()
        {
            return this.dbSet.Create();
        }
    }
}
