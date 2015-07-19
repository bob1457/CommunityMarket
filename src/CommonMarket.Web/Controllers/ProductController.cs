using System;
using System.Web.Mvc;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Services.ProductServices;

namespace CommonMarket.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }


        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetails(int id)
        {

            return View();
        }






        [HttpPost]
        public void AddProduct(Product product)
        {
            var newProduct = new Product();

            if (ModelState.IsValid)
            {
                newProduct.ProductName = product.ProductName;
                newProduct.ProductDescLong = product.ProductDescLong;
                //more attributes/properties

                newProduct.CreateDate = DateTime.Now;
                newProduct.UpdateDate = DateTime.Now;

                _productServices.AddNewProduct(product);
            }

        }
    }
}