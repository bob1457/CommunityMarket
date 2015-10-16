using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.Core.Interface
{
    public interface IPromotionService
    {
        void AddDiscount(Discount discount);
        void AddCoupon(Coupon coupon);
        IEnumerable<Discount> FindAllDiscounts();
        IEnumerable<Coupon> FindAllCoupons();
        IEnumerable<Discount> GetDiscountsBy(int id);
        IEnumerable<Coupon> GetCouponsBy(int id);
    }
}