using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    interface ICustomerRepository 
    {
        public IEnumerable<Customer> GetCustomers();
        public Customer GetCustomer(int id);
        public void UpdateCustomer(Customer customer);
        public void CreateCustomer(Customer customer);
        public void DeleteCustomer(int id);
    }
}
