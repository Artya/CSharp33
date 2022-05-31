using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    class CustomerDataProcessor : IDBObjectDataProcessor
    {
        private static ICustomerRepository customerRepository = default;

        public CustomerDataProcessor(ICustomerRepository repository)
        {
            if (customerRepository == null)
                customerRepository = repository;
        }

        public string MenuTitle { get => "CUSTOMER MENU"; }

        public static Customer SelectCustomer()
        {
            var customers = (List<Customer>)customerRepository.GetCustomers();
            var customerID = UserInteraction.ChooseElementFromList<Customer>(customers, "Choose customer: ");
            var selectedCustomer = customers[customerID];
            return selectedCustomer;
        }

        public void CreateObject()
        {
            var newCustomer = new Customer
            {
                Name = UserInteraction.GetString("Enter customer name"),
                Phone = UserInteraction.GetString("Enter customer phone"),
                EMail = UserInteraction.GetString("Enter customer EMail"),
                Details = UserInteraction.GetString("Enter customer Details")
            };

            customerRepository.CreateCustomer(newCustomer);
        }

        public void DeleteObject()
        {
            var id = UserInteraction.GetInt("Enter customer ID");
            customerRepository.DeleteCustomer(id);
        }

        public void EditObject()
        {
            var customer = GetCustomerByID();

            if (customer == null)
                return;

            var fields = VariableFields.GetChangingFields(typeof(Customer));

            while (true)
            {
                var index = UserInteraction.ChooseElementFromList<VariableFields>(fields, "Choose field to edit");

                if (index == fields.Count - 1)
                {
                    if (customer.IsModified)
                    {
                        customerRepository.UpdateCustomer(customer);
                    }

                    return;
                }

                var newValue = UserInteraction.GetString($"Enter new {fields[index]} to {customer}");

                switch (fields[index].FieldName)
                {
                    case "Name":
                        customer.Name = newValue;
                        break;

                    case "Phone":
                        customer.Phone = newValue;
                        break;

                    case "EMail":
                        customer.EMail = newValue;
                        break;

                    case "Details":
                        customer.Details = newValue;
                        break;
                }

                customer.SetModifiedOn();
            }
        }

        public void ReadObject()
        {
            var customer = GetCustomerByID();
            Console.WriteLine(customer);
        }

        private static Customer GetCustomerByID()
        {
            var id = UserInteraction.GetInt("Enter customer ID");
            return GetCustomerByID(id);
        }
        public static Customer GetCustomerByID(int id)
        {
            return customerRepository.GetCustomer(id);
        }

        public void ShowAllObjects()
        {
            var customers = customerRepository.GetCustomers();
            DatabaseTable.ShowObjectsList(customers, "Customers:");
        }
    }
}
