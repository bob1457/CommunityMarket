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
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _uow;

        public CustomerService(IRepository<Customer> customerRepository, IUnitOfWork uow)
        {
            _customerRepository = customerRepository;
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

        public void DeleteCustomer()
        {
            
        }


        #endregion


    }
}
