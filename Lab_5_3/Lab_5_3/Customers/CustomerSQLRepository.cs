using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab_5_3
{
    class CustomerSQLRepository : ICustomerRepository, ISQLRepository
    {
        public SqlConnection Connection { get; }
        public string SchemaName => "DBO";
        public string TableName => "Customers";

        public CustomerSQLRepository(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public void CreateCustomer(Customer customer)
        {
            SQLRepositoryHelper.ExecuteInsertingObject(customer, this);
        }

        public void DeleteCustomer(int id)
        {
            SQLRepositoryHelper.ExecuteDeletingObject(id, this);
        }

        public Customer GetCustomer(int id)
        {
            var reader = SQLRepositoryHelper.ExecuteGetObjectByID(id,this);

            if (reader == null)
                return null;

            var customer = GetCustomerFromReader(reader);
            reader.Close();
            return customer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            var customers = new List<Customer>();

            var reader = SQLRepositoryHelper.GetSeletionAllReader(this);

            while (reader.Read())
            {
                var currentCustomer = GetCustomerFromReader(reader);
                customers.Add(currentCustomer);
            }

            reader.Close();

            return customers;
        }

        public Customer GetCustomerFromReader(SqlDataReader reader)
        {
            var currentCustomer = new Customer();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var fieldName = reader.GetName(i);

                switch (fieldName)
                {
                    case nameof(Customer.ID):
                        currentCustomer.ID = (int)reader[i];
                        break;

                    case nameof(Customer.Name):
                        currentCustomer.Name = (string)reader[i];
                        break;

                    case nameof(Customer.Phone):
                        currentCustomer.Phone = (string)reader[i];
                        break;

                    case nameof(Customer.EMail):
                        currentCustomer.EMail = (string)reader[i];
                        break;

                    case nameof(Customer.Details):
                        currentCustomer.Details = (string)reader[i];
                        break;

                    default:
                        Console.WriteLine($"Field  {fieldName} isn`t in class Customer, or it filling not implemented");
                        break;
                }
            }

            return currentCustomer;
        }
        public void UpdateCustomer(Customer customer)
        {
            SQLRepositoryHelper.ExecuteUpdatingObject(customer, this);
        }
    }
}
