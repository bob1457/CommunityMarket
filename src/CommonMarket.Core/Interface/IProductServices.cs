using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.Core.Interface
{
    public interface IProductServices
    {
        IEnumerable<Entities.Product> FindAllProducts();
        Entities.Product FindProductById(int id);
        IEnumerable<Entities.Product> FindProductByCategory(int id); //id: category id
        IEnumerable<Entities.Product> GetProductOnPromotion(int id, int discountId); //id: supplier id
        IEnumerable<Entities.Product> GetProductNotOnPromotion(int id); //id: supplier id
        IEnumerable<Entities.Product> ListAllProductsOnPromotion();

        void AddNewProduct(Entities.Product product, ProductCategory category);
        void AddAdditionalImage(AdditionalProductImg image);

        IEnumerable<AdditionalProductImg> GetImgListByProduct(int id);
        void UpdateProduct(Entities.Product product);

        void UpdateProductImg(Entities.Product product);

        void DeleteProduct(int id);
    }
}