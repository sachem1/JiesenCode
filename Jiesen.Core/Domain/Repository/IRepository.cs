using Jiesen.Core.Model;

namespace Jiesen.Core.Domain.Repository
{
    public interface IRepository<TEntity> : ICommandRepository<TEntity>, IQueryRepository<TEntity>
        where TEntity : class, IEntity
    {

    }
}