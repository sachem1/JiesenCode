
using Jiesen.Core.Domain.Repository;
using Jiesen.Core.Domain.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jiesen.Core.Domain.StaticRepository;
using Jiesen.Core.Model;

namespace Jiesen.EntityFramework.Repository
{
    public class StaticRepository<TEntity> : BaseStaticQueryRepository<TEntity> where TEntity : class, IEntity
    {
        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
