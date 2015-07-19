using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.DataAccess;

namespace CommonMarket.Services.UserServices
{
    public class MerchantService : IMerchantService
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IUnitOfWork _uow;

        public MerchantService(IRepository<Supplier> supplierRepository, IUnitOfWork uow )
        {
            _supplierRepository = supplierRepository;
            _uow = uow;
        }


        #region Service Operations

        public IEnumerable<Supplier> FindAllSuppliers()
        {
            return _supplierRepository.GetAll();
        }

        public Supplier FindSupplierById(int id)
        {
            return _supplierRepository.GetAll().FirstOrDefault(c => c.Id == id);
        }

        public void AddSupplier(Supplier supplier)
        {
            try
            {
                _supplierRepository.Add(supplier);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed adding the suplier", ex);
            }
        }


        public void UpdateSupplier(int id) //customer id
        {
            try
            {
                Supplier supplier = _supplierRepository.GetAll().FirstOrDefault(i => i.Id == id);
                //customer.UpdateDate = DateTime.Now;
                //category.DepartmentId = 1;

                _supplierRepository.Update(supplier);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed updating the category", ex);
            }
        }

        public void DeleteSupplier()
        {

        }


        #endregion


        public Supplier FindSupplierBy(int id)
        {
            return _supplierRepository.FindBy(p => p.UserProfileId == id).FirstOrDefault();
        }
    }
}
