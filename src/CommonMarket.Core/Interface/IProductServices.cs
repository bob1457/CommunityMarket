using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.Services.ProductServices
{
    public interface IProductServices
    {
        IEnumerable<Product> FindAllProducts();
        Product FindProductById(int id);
        IEnumerable<Product> FindProductByCategory(int id); //id: category id

        void AddNewProduct(Product product, ProductCategory category);
        void AddAdditionalImage(AdditionalProductImg image);

        IEnumerable<AdditionalProductImg> GetImgListByProduct(int id);
        void UpdateProduct(Product product);

        void UpdateProductImg(Product product);

        void DeleteProduct(int id);
    }
}