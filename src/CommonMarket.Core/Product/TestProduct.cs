using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonMarket.Core.Interface;

namespace CommonMarket.Core.Product
{
    #region Testing

    public class TestProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { set; get; }
    }

    //public class ValueCalculator
    //{
    //    public decimal ValueProducts(IEnumerable<Product> products)
    //    {
    //        return products.Sum(p => p.Price);
    //    }
    //}

    public class ShoppingCart
    {
        private IValueCalculator calc;

        public ShoppingCart(IValueCalculator calcParam)
        {
            calc = calcParam;
        }

        public IEnumerable<TestProduct> Products { get; set; }

        public decimal CalculateProductTotal()
        {
            return calc.ValueProducts(Products);
        }
    }

    #endregion
}
