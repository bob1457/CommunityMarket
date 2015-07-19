using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonMarket.DataAccess.Base
{
    public interface IBaseRepositoryAsync<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        
    }
}
