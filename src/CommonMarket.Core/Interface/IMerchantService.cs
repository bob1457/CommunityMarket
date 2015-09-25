using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.Core.Interface
{
    public interface IMerchantService
    {
        IEnumerable<Supplier> FindAllSuppliers();
        Supplier FindSupplierById(int id);

        Supplier FindSupplierBy(int id);

        IEnumerable<int> GetAllSupplierId();

        IEnumerable<CartItem> GetCartItemsBySupplier(int id, int cid); //id is supplier id, cid is cart id

        void AddSupplier(Supplier supplier);

        void UpdateSupplier(int id); //supplier id
            

        void DeleteSupplier();
    }
}