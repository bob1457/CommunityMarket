using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonMarket.DataAccess
{
    public interface IRepositoryAsync<T> : IRepository<T> where T : class
    {
        Task<T> FindAsync(params object[] keyValue);
        Task<T> FindAsync(CancellationToken cancellationToken, params object[] keyValue);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

    }
}
