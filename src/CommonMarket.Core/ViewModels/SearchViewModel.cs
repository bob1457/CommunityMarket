using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMarket.Core.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<SearchViewModel> SearchProductList { get; set; }

        public int ProductId { get; set; }
        
        public string ProductName { get; set; }
        
        public string ProductDescShort { get; set; }
        
        public string ProductDescLong { get; set; }
        
        public string ProductImgLargeUrl { get; set; }
        
        public string ProductImgSmallUrl { get; set; }

        public int SupplierId { get; set; }
        
        public string UnitName { get; set; }

        public int? QuantityPerUnit { get; set; }
        
        public decimal UnitPrice { get; set; }

        
        public IEnumerable<SearchViewModel> SearchMerchantList { get; set; }

        public string ContactFirstName { get; set; }
       
        public string ContactLastName { get; set; }
       
        public string CompanyIconImgUrl { get; set; } //use as vendor email/username(for login)
        
        


    }
}
