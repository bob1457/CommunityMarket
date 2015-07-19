using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.Core.Interface
{
    public interface IMerchantService
    {
        IEnumerable<Supplier> FindAllSuppliers();
        Supplier FindSupplierById(int id);

        Supplier FindSupplierBy(int id);

        void AddSupplier(Supplier supplier);

        void UpdateSupplier(int id); //supplier id
            

        void DeleteSupplier();
    }
}