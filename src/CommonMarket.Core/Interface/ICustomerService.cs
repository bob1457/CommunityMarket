using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.Core.Interface
{
    public interface ICustomerService
    {
        IEnumerable<Customer> FindAllCustomers();
        Customer FindCustomerById(int id);
        Customer FindCustomerBy(int id);

        void AddCustomer(Customer customer);
        void UpdateCustomer(int id); //customer id
        void UpdateCustomerInfo(Customer customer);
        void UpdateCustomerInfo(Customer customer, CustomerAddress billingAddress,
            CustomerAddress shippingAddress);

        void DeleteCustomer();

        CustomerAddress FindCustomerAddress(int id, int addrType);

    }
}