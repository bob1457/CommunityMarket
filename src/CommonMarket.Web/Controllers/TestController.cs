using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMarket.Core.Interface;
using CommonMarket.Core.Product;

namespace CommonMarket.Web.Controllers
{
    public class TestController : Controller
    {
        private IValueCalculator calc;

        //Test Data
        private TestProduct[] products = {
            new TestProduct {Name = "Kayak", Category = "Watersports", Price = 275M},
            new TestProduct {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new TestProduct {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
            new TestProduct {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };

        public TestController(IValueCalculator calculator)
        {
            calc = calculator ;
        }


        // GET: Test
        public ActionResult Index()
        {
            ShoppingCart cart = new ShoppingCart(calc)
            {
                Products = products
            };

            decimal totalValue = cart.CalculateProductTotal();

            return View(totalValue);
        }
    }
}