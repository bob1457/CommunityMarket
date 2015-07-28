using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.Services.ProductServices
{
    public interface IProductServices
    {
        IEnumerable<Product> FindAllProducts();
        Product FindProductById(int id);
        void AddNewProduct(Product product, ProductCategory category);
        void UpdateProduct(Product product);

        void UpdateProductImg(Product product);

        void DeleteProduct(int id);
    }
}