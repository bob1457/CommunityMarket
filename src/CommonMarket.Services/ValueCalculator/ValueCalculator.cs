using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonMarket.Core.Interface;


namespace CommonMarket.Services.ValueCalculator
{
    public class ValueCalculator : IValueCalculator
    {
        public decimal ValueProducts(IEnumerable<Core.Product.TestProduct> products)
        {
            //throw new NotImplementedException();
            return products.Sum(p => p.Price);
        }

        
    }
}
