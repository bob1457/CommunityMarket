using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMarket.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;

        //public IRepository<T> GetRepository<T>() where T : class
        //{
        //    throw new NotImplementedException();
        //}
        public UnitOfWork(IDbContext context)
        {
            _dbContext = context;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool displsing)
        {
            _dbContext.Dispose();
            _dbContext = null;
        }
    }
}
