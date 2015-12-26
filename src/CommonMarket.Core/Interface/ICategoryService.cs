using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.Core.Interface
{
    public interface ICategoryService
    {
        IEnumerable<ProductCategory> FindAllCategories();

        ProductCategory FindCategoryById(int id);

        void AddNewCategory(ProductCategory productCategory);

        //void UpdateCategory(int id);
        void UpdateCategory(ProductCategory productCategory);

        void DeleteCategory(int id);
    }
}