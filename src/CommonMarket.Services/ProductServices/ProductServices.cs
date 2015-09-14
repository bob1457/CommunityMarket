using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.DataAccess;

namespace CommonMarket.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> _proRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRepository<ProductCategory> _categoryRepository;
        private readonly IRepository<AdditionalProductImg> _addImageRepository;

        private readonly IUnitOfWork _uow;

        public ProductServices(IRepository<Product> proRepository, IProductRepository productRepository, 
            IRepository<ProductCategory> categoryRepository, IRepository<AdditionalProductImg> addImageRepository, IUnitOfWork uow)
        {
            _proRepository = proRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _addImageRepository = addImageRepository;

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

        public void AddAdditionalImage(AdditionalProductImg image)
        {
            _addImageRepository.Add(image);

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

        public void UpdateProductImg(Product product)
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



        public IEnumerable<Product> FindProductByCategory(int id)
        {
            //throw new NotImplementedException();
            return _productRepository.FindByCategory(id);
        }


        public IEnumerable<AdditionalProductImg> GetImgListByProduct(int id)
        {
            return _addImageRepository.GetAll().Where(p => p.ProductId == id);
        }


        public void DeleteProduct(int id)
        {

        }

        //public Supplier GetSupplierByProduct(int id) //id is product id
        //{
            
           
        //}


        #endregion



    }
}
