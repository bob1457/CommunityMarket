using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMarket.Core.Interface
{
    public class ValueCalculator : IValueCalculator
    {
        public decimal ValueProducts(IEnumerable<Product.TestProduct> products)
        {
            //throw new NotImplementedException();
            return products.Sum(p => p.Price);
        }
    }
}
