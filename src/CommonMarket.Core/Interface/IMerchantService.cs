using System.Collections.Generic;
using CommonMarket.Core.Entities;
using CommonMarket.Core.ViewModels;

namespace CommonMarket.Core.Interface
{
    public interface IMerchantService
    {
        IEnumerable<Supplier> FindAllSuppliers();
        Supplier FindSupplierById(int id);
        Supplier FindSupplierBy(int id);
        IEnumerable<int> GetAllSupplierId();
        IEnumerable<CartItem> GetCartItemsBySupplier(int id, int cid); //id is supplier id, cid is cart id
        IEnumerable<MerchantFeePayment> GetBillPaymentListByVendor(int id);
        IEnumerable<CustomerViewModel> ListCustomersByVendor(int id);
        IEnumerable<Order> GetOrderAndOrderItemsByCustomer(int id);
        IEnumerable<OrderItem> GetOrderItemsByCustomer(int id, int sId);

        void AddSupplier(Supplier supplier);
        void AddBillPayment(MerchantFeePayment payment);
        void UpdateSupplier(int id); //supplier id
        void UpdateSupplierLogoUrl(int id, string url); 
        void UpdateSupplierInfo(Supplier supplier);
    

        void DeleteSupplier();
    }
}