using System;
using System.Collections.Generic;
using System.Linq;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.DataAccess;

namespace CommonMarket.Services.UserServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICustomerRepository _custRepository;
        private readonly IRepository<CustomerAddress> _customerAddressRepository;
        private readonly IUnitOfWork _uow;

        public CustomerService(IRepository<Customer> customerRepository, ICustomerRepository custRepository, 
            IRepository<CustomerAddress> customerAddress, IUnitOfWork uow)
        {
            _customerRepository = customerRepository;
            _custRepository = custRepository;
            _customerAddressRepository = customerAddress;
            _uow = uow;
        }


        #region Service Operations

        public IEnumerable<Customer> FindAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer FindCustomerById(int id)
        {
            return _customerRepository.GetAll().FirstOrDefault(c => c.Id == id);
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                _customerRepository.Add(customer);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed adding the customer", ex);
            }
        }


        public void UpdateCustomer(int id) //customer id
        {
            try
            {
                Customer customer = _customerRepository.GetAll().FirstOrDefault(i => i.Id == id);
                //customer.UpdateDate = DateTime.Now;
                //category.DepartmentId = 1;

                _customerRepository.Update(customer);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed updating the customer", ex);
            }
        }


        public void UpdateCustomerInfo(Customer customer)
        {
            try
            {
                _customerRepository.Update(customer);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed updating the customer", ex);
            }
        }

        public void UpdateCustomerInfo(Customer customer, CustomerAddress billingAddress,
            CustomerAddress shippingAddress)
        {
            try
            {
                _customerRepository.Update(customer);

                var bAddress = _customerAddressRepository.GetAll().FirstOrDefault(c => c.CustomerId == customer.Id && c.AddressType == 1);

                if (bAddress != null)
                {
                    //_custRepository.UpdateCusotmerAddress(customer.Id, billingAddress);

                    bAddress.AddressCity = billingAddress.AddressCity;
                    bAddress.AddressStreet = billingAddress.AddressStreet;
                    bAddress.AddressProState = billingAddress.AddressProState;
                    bAddress.AddressPostZipCode = billingAddress.AddressPostZipCode;
                    bAddress.AddressCountry = billingAddress.AddressCountry;


                    _customerAddressRepository.Update(bAddress);
                }
                else
                {
                    //_custRepository.AddCusotmerAddress(customer.Id, billingAddress);
                    _customerAddressRepository.Add(billingAddress);
                }

                var sAddress = _customerAddressRepository.GetAll().FirstOrDefault(c => c.CustomerId == customer.Id && c.AddressType == 2);

                if (sAddress != null)
                {
                    //_custRepository.UpdateCusotmerAddress(customer.Id, billingAddress);
                    sAddress.AddressCity = shippingAddress.AddressCity;
                    sAddress.AddressStreet = shippingAddress.AddressStreet;
                    sAddress.AddressProState = shippingAddress.AddressProState;
                    sAddress.AddressPostZipCode = shippingAddress.AddressPostZipCode;
                    sAddress.AddressCountry = shippingAddress.AddressCountry;

                    _customerAddressRepository.Update(sAddress);
                }
                else
                {
                    //_custRepository.AddCusotmerAddress(customer.Id, shippingAddress);
                    _customerAddressRepository.Add(shippingAddress);
                }

                _uow.Save();
            }
            catch (Exception ex)
            {

                throw new Exception("Failed updating the customer", ex);
            }
        }

        public void DeleteCustomer()
        {
            
        }


        public Customer FindCustomerBy(int id)
        {
            return _customerRepository.FindBy(p => p.UserProfileId == id).FirstOrDefault();
        }

        public CustomerAddress FindCustomerAddress(int id, int addrType) //id: custoemr id
        {
            return _custRepository.FindCustomerAddress(id, addrType);
        }


        #endregion


    }
}
