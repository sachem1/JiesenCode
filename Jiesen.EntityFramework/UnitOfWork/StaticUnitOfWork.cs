using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jiesen.Core.Domain.UOW;

namespace Jiesen.EntityFramework.UnitOfWork
{
    class StaticUnitOfWork:IStaticEfUnitOfWork
    {
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void CommitAndRefreshChanges()
        {
            throw new NotImplementedException();
        }

        public void RollbackChanges()
        {
            throw new NotImplementedException();
        }

        public TDbContext GetOrCreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            throw new NotImplementedException();
        }
    }
}
