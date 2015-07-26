using System.Collections.Generic;
using CommonMarket.core.Entities;

namespace CommonMarket.Core.Interface
{
    public interface ICustomerService
    {
        IEnumerable<Customer> FindAllCustomers();

        Customer FindCustomerById(int id);

        Customer FindCustomerBy(int id);

        void AddCustomer(Customer customer);

        void UpdateCustomer(int id); //customer id

        void DeleteCustomer();
    }
}