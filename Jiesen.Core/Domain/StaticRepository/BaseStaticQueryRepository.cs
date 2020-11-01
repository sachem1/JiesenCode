using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jiesen.Core.Model;

namespace Jiesen.Core.Domain.StaticRepository
{
    public abstract class BaseStaticQueryRepository<TEntity> : IStaticQueryRepository<TEntity> where TEntity : class, IEntity
    {
        public abstract void Dispose();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(long id)
        {
            return Get(id);
        }

        public IQueryable<TEntity> GetAll(bool isReadOnly = false)
        {
            return GetAll(false);
        }
    }
}
