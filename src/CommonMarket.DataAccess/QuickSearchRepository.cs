using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CommonMarket.Core.ViewModels;

namespace CommonMarket.DataAccess
{
    public interface IQuickSearchRepository
    {
        IEnumerable<QuickSearchViewModel> Find(string term);
        IEnumerable<QuickSearchViewModel> GetAll();
        IEnumerable<QuickSearchViewModel> FindBy(System.Linq.Expressions.Expression<System.Func<QuickSearchViewModel, bool>> predicate);
        void Add(QuickSearchViewModel entity);
        void Delete(QuickSearchViewModel entity);
        void DeleteAll(IEnumerable<QuickSearchViewModel> entity);
        void Update(QuickSearchViewModel entity);
        bool Any();
    }

    public class QuickSearchRepository : Repository<QuickSearchViewModel>, IQuickSearchRepository
    {
        public QuickSearchRepository(IDbContext context)
            : base(context)
        {

        }

        public IEnumerable<QuickSearchViewModel> Find(string term)
        {
            using (var context = new CommunityMarketContext())
            {
                var products =
                    from product in
                        context.Products.Where(
                            p =>
                                p.ProductName.ToUpper().Contains(term.ToUpper()) ||
                                p.ProductDescLong.ToUpper().Contains(term.ToUpper()) || p.ProductDescShort.ToUpper().Contains(term.ToUpper()))
                        select new QuickSearchViewModel
                        {
                            Name = product.ProductName,
                            Id = product.Id,
                            ImgUrl = product.ProductImgSmallUrl,
                            Path = "/Product/ProductDetails/"
                        };

                return products.ToList();
            }
        }
    }
}
