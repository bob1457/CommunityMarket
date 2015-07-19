using System;
using System.Collections.Generic;
using System.Linq;

using CommonMarket.Core.Entities;
using CommonMarket.DataAccess;

namespace CommonMarket.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _uow;

        public ProductServices(IRepository<Product> productRepository, IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _uow = uow;
        }

        #region Service Operations

        public IEnumerable<Product> FindAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product FindProductById(int id)
        {
            return _productRepository.GetAll().FirstOrDefault(c => c.Id == id);
        }

        public void AddNewProduct(Product product)
        {
            try
            {
                product.CreateDate = DateTime.Now;

                _productRepository.Add(product);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed adding the product", ex);
            }

        }

        public void UpdateProduct(Product product)
        {
            try
            {
                //ProductCategory category = _categoryRepository.GetAll().FirstOrDefault(i => i.Id == id);
                product.UpdateDate = DateTime.Now;
                //category.DepartmentId = 1;

                _productRepository.Update(product);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed updating the product", ex);
            }
        }

        public void DeleteProduct(int id)
        {

        }


        #endregion


    }
}
