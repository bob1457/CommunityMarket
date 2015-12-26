using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CommonMarket.Core.Interface
{
    public interface IValueCalculator
    {
        decimal ValueProducts(IEnumerable<Product.TestProduct> products);
    }
}
