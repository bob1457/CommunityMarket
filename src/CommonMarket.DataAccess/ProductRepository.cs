﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CommonMarket.Core.Entities;

namespace CommonMarket.DataAccess
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> FindBy(System.Linq.Expressions.Expression<System.Func<Product, bool>> predicate);
        IEnumerable<Product> FindByCategory(int id); 
        void Add(Product product, ProductCategory category);
        void Delete(Product entity);
        void DeleteAll(IEnumerable<Product> entity);
        void Update(Product entity);
        bool Any();
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IDbContext context)
            : base(context)
        {

        }
        

        public void Add(Product product, ProductCategory category)
        {
            try
            {
                using (var context = new CommunityMarketContext())
                {
                    if (category.Id > 0)
                    {
                        context.Entry(category).State = EntityState.Unchanged;
                    }

                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw; 
            }
            


            //throw new NotImplementedException();
            
            
        }

        public void GetSupplierByProduct(int id)
        {
            using (var context = new CommunityMarketContext())
            {
                
            }
        }


        public IEnumerable<Product> FindByCategory(int id)
        {
            //throw new NotImplementedException();
            try
            {
                
                using (var context = new CommunityMarketContext())
                {
                   return context.Products.Where(p => p.ProductCategories.Any(i => i.Id == id)).ToList();
                }

                
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
