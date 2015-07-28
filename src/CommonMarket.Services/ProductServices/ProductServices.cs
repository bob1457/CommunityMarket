using System;
using System.Collections.Generic;
using System.Linq;

using CommonMarket.Core.Entities;
using CommonMarket.DataAccess;

namespace CommonMarket.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> _proRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRepository<ProductCategory> _categoryRepository;

        private readonly IUnitOfWork _uow;

        public ProductServices(IRepository<Product> proRepository, IProductRepository productRepository, IRepository<ProductCategory> categoryRepository, IUnitOfWork uow)
        {
            _proRepository = proRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;

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

        public void AddNewProduct(Product product, ProductCategory category)
        {
            try
            {
                //var existCategory = new ProductCategory() 
                //{
                //    Id = category.Id
                //}; // _categoryRepository.GetAll().Where(i => i.Id == category.Id);

                //product.ProductCategories.Add(existCategory);

                product.ProductCategories.Add(category);
                
                _productRepository.Add(product, category);
                //_uow.Save();

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

                _proRepository.Update(product);
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
