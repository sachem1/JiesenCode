using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jiesen.Core.Model;

namespace Jiesen.Core.Domain.StaticRepository
{
    public abstract class BaseStaticQueryRepository<TEntity> :IStaticQueryRepository<TEntity> where TEntity : class, IEntity
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public long Get(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll(bool isReadOnly = false)
        {
            throw new NotImplementedException();
        }
    }
}
