using TradeYourPhone.Core.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Test.DummyRepositories
{
    class GenericRepositoryMock<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected Dictionary<int, TEntity> _data = new Dictionary<int, TEntity>();
        protected Expression<Func<TEntity, int>> _identityExpression;

        public GenericRepositoryMock(Dictionary<int, TEntity> data, Expression<Func<TEntity, int>> identityExpression)
        {
            _data = data;
            _identityExpression = identityExpression;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _data.Values.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            var entity = default(TEntity);
            _data.TryGetValue((int)id, out entity);
            return entity;
        }

        public virtual void Insert(TEntity entity)
        {
            var newKey = _data.Count == 0 ? 1 : _data.Keys.Max() + 1;
            SetIdentityValue(entity, newKey);
            _data.Add(newKey, entity);
        }

        public virtual void Delete(object Id)
        {
            _data.Remove((int)Id);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            var identityValue = GetIdentityValue(entityToDelete);
            _data.Remove(identityValue);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            var identityValue = GetIdentityValue(entityToUpdate);

            if (!_data.ContainsKey(identityValue))
            {
                throw new Exception("Cannot update");
            }
            _data[identityValue] = entityToUpdate;
        }

        protected virtual string GetIdentityName()
        {
            return ((MemberExpression)_identityExpression.Body).Member.Name;
        }

        protected virtual int GetIdentityValue(TEntity item)
        {
            return (int)item.GetType().GetProperty(GetIdentityName()).GetValue(item, null);
        }

        protected virtual void SetIdentityValue(TEntity item, object value)
        {
            item.GetType().GetProperty(GetIdentityName()).SetValue(item, value, null);
        }
    }
}
