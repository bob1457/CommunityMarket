using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.Core.ViewModels;
using CommonMarket.DataAccess;

namespace CommonMarket.Services.UserServices
{
    public class MerchantService : IMerchantService
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IRepository<MerchantFeePayment> _merchanteFeeRepository;
        private readonly IUnitOfWork _uow;

        public MerchantService(IRepository<Supplier> supplierRepository, IMerchantRepository merchantRepository, 
            IRepository<MerchantFeePayment> merchanteFeeRepository, 
            IUnitOfWork uow )
        {
            _supplierRepository = supplierRepository;
            _merchantRepository = merchantRepository;
            _merchanteFeeRepository = merchanteFeeRepository;
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


        public void UpdateSupplierLogoUrl(int id, string url) //customer id
        {
            try
            {
                Supplier supplier = _supplierRepository.GetAll().FirstOrDefault(i => i.Id == id);
                //customer.UpdateDate = DateTime.Now;
                //category.DepartmentId = 1;
                supplier.CompanyLogoImgUrl = url;

                _supplierRepository.Update(supplier);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed updating the url", ex);
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

        public IEnumerable<int> GetAllSupplierId()
        {
            return _merchantRepository.GetAllSupplierIds();
        }

        public IEnumerable<CartItem> GetCartItemsBySupplier(int id, int cid)
        {
            return _merchantRepository.GetCartItmesBySupplier(id, cid);
        }

        public void AddBillPayment(MerchantFeePayment payment)
        {
            _merchanteFeeRepository.Add(payment);
        }

        public IEnumerable<MerchantFeePayment> GetBillPaymentListByVendor(int id) //id: supplier id
        {
            return _merchanteFeeRepository.GetAll().Where(s => s.SupplierId == id);
        }

        public void UpdateSupplierInfo(Supplier supplier)
        {
            _supplierRepository.Update(supplier);
            //_uow.Save();
        }

        public IEnumerable<CustomerViewModel> ListCustomersByVendor(int id) //id: supplier id
        {
            return _merchantRepository.GetCustomerList(id);
        }

        public IEnumerable<Order> GetOrderAndOrderItemsByCustomer(int id)
        {
            return _merchantRepository.GetOrderAndOrderItemsByCusotmer(id);
        }

        public IEnumerable<OrderItem> GetOrderItemsByCustomer(int id, int sId )
        {
            return _merchantRepository.GetOrderItemsByCustomer(id, sId);
        }
    }
}
