using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.DataAccess;

namespace CommonMarket.Services.MarketingServices
{
    public class PromotionService : IPromotionService
    {
        private readonly IRepository<Discount> _discountRepository;
        private readonly IRepository<Coupon> _couponRepository;
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IRepository<Discount> discountRepository, IRepository<Coupon> couponRepository, IPromotionRepository promotionRepository  )
        {
            _discountRepository = discountRepository;
            _couponRepository = couponRepository;
            _promotionRepository = promotionRepository;
        }

        public void AddDiscount(Discount discount)
        {
            discount.CreateDate = DateTime.Now;
            discount.UpdateDate = DateTime.Now;

            _discountRepository.Add(discount);
        }

        public void AddCoupon(Coupon coupon)
        {
            coupon.CreateDate = DateTime.Now;
            coupon.UpdateDate = DateTime.Now;

            _couponRepository.Add(coupon);
        }

        public IEnumerable<Discount> FindAllDiscounts()
        {
            return _discountRepository.GetAll();
        }

        public IEnumerable<Coupon> FindAllCoupons()
        {
            return _couponRepository.GetAll();
        }

        public IEnumerable<Discount> GetDiscountsBy(int id)
        {
            //return _discountRepository.GetAll().Where(s => s.SupplierId == id);
            return _promotionRepository.ListAllDiscountsBySupplier(id);
        }

        public IEnumerable<Coupon> GetCouponsBy(int id)
        {
            return _couponRepository.GetAll().Where(s => s.SupplierId == id);
        }

    }
}
