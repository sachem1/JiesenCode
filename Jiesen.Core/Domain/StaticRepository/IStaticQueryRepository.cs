using Jiesen.Core.Domain.Repository;
using Jiesen.Core.Model;

namespace Jiesen.Core.Domain.StaticRepository
{
    public interface IStaticQueryRepository<TEntity>: IQueryRepository<TEntity> where TEntity : class, IEntity
    {
        
    }
}