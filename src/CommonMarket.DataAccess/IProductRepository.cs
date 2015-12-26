using System;
using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.DataAccess
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> FindBy(System.Linq.Expressions.Expression<Func<Product, bool>> predicate);
        void Add(Product entity);
        void Delete(Product entity);
        void DeleteAll(IEnumerable<Product> entity);
        void Update(Product entity);
        bool Any();
    }
}