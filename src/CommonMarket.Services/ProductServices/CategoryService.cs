using System;
using System.Collections.Generic;
using System.Linq;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.DataAccess;

namespace CommonMarket.Services.ProductServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<ProductCategory> _categoryRepository;
        private readonly IUnitOfWork _uow;

        //Constructor and DI
        public CategoryService(IRepository<ProductCategory> categoryRepository, IUnitOfWork uow )
        {
            _categoryRepository = categoryRepository;
            _uow = uow;
        }

        #region Service Operations

        public IEnumerable<ProductCategory> FindAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public ProductCategory FindCategoryById(int id)
        {
            return _categoryRepository.GetAll().FirstOrDefault(c => c.Id == id);
        }

        public void AddNewCategory(ProductCategory productCategory)
        {
            try
            {
                productCategory.CreateDate = DateTime.Now;
                

                _categoryRepository.Add(productCategory);
                //_uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed adding the category", ex);
            }
           
        }

        public void UpdateCategory(ProductCategory category)
        {
            try
            {
                //ProductCategory category = _categoryRepository.GetAll().FirstOrDefault(i => i.Id == id);
                category.UpdateDate = DateTime.Now;
                //category.DepartmentId = 1;

                _categoryRepository.Update(category);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed updating the category", ex);
            }
        }

        public void DeleteCategory(int id)
        {

        }

        #endregion

    }
}
