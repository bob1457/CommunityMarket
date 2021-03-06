﻿using System;
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
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<AdditionalProductImg> _addImageRepository;

        private readonly IUnitOfWork _uow;

        public ProductServices(IRepository<Product> proRepository, IProductRepository productRepository, 
            IRepository<ProductCategory> categoryRepository, IRepository<Comment> commentRepository, IRepository<AdditionalProductImg> addImageRepository, IUnitOfWork uow)
        {
            _proRepository = proRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
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

        public IEnumerable<Product> GetProductOnPromotion(int id, int discountId)
        {
            //return _proRepository.FindBy(p=> p.SupplierId == id).Where(p=>p.DiscountId != null || p.DiscountId != 0);
            return _productRepository.GetProductOnPromotionBySupplier(id, discountId);
        }
        
        public IEnumerable<Product> GetProductNotOnPromotion(int id)
        {
            //return _proRepository.FindBy(p => p.DiscountId == null || p.DiscountId == 0 && p.SupplierId == id);
            return _productRepository.GetProductNotOnPromotionBySupplier(id);
        }

        public IEnumerable<Product> ListAllProductsOnPromotion()
        {
            return _productRepository.FindAllProductsOnPromotion();
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

        #region Social

        public void AddCommentsOnProduct(Comment comment)
        {
            //comment.CreateDate = DateTime.Now;
            //var newComment = new Comment();

            //newComment.EntityRecordId = comment.EntityRecordId;
            //newComment.CommentBody = comment.CommentBody;
            //newComment.DomainEntityId = 1;
            //newComment.CommentedBy = comment.CommentedBy;
            //newComment.CreateDate = DateTime.Now;

            _commentRepository.Add(comment);
        }


        public void AddCommentsOnSupplier(Comment comment)
        {
            //comment.CreateDate = DateTime.Now;
            //var newComment = new Comment();

            //newComment.EntityRecordId = comment.EntityRecordId;
            //newComment.CommentBody = comment.CommentBody;
            //newComment.DomainEntityId = 1;
            //newComment.CommentedBy = comment.CommentedBy;
            //newComment.CreateDate = DateTime.Now;

            _commentRepository.Add(comment);
        }


        public IEnumerable<Comment> GetComments(int id) //id: product id
        {
            return _commentRepository.GetAll().Where(p => p.EntityRecordId == id).OrderByDescending(d=>d.CreateDate);
        }

        #endregion

    }
}
