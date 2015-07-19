using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.Core.Interface
{
    public interface ICustomerService
    {
        IEnumerable<Customer> FindAllCustomers();

        Customer FindCustomerById(int id);

        void AddCustomer(Customer customer);

        void UpdateCustomer(int id); //customer id

        void DeleteCustomer();
    }
}