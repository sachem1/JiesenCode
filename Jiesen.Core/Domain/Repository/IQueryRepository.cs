using System;
using System.Linq;
using Jiesen.Core.Model;

namespace Jiesen.Core.Domain.Repository
{
    public interface IQueryRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        /// <summary>
        /// 获取实体的个数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        long Get(long id);

        /// <summary>
        ///获取所有的实体
        /// </summary>
        /// <returns>List of selected elements</returns>
        IQueryable<TEntity> GetAll(bool isReadOnly = false);
    }
}