using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab_5_3
{
    class SupplierSQLRepository : ISupplierRepository, ISQLRepository
    {
        public SqlConnection Connection { get; }
        public string SchemaName => "DBO";
        public string TableName => "Suppliers";

        public SupplierSQLRepository(SqlConnection connection)
        {
            this.Connection = connection;
        }
        public void CreateSupplier(Supplier supplier)
        {
            SQLRepositoryHelper.ExecuteInsertingObject(supplier, this);
        }

        public void DeleteSupplier(int id)
        {
            SQLRepositoryHelper.ExecuteDeletingObject(id, this);
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            var suppliers = new List<Supplier>();

            var reader = SQLRepositoryHelper.GetSeletionAllReader(this);

            while (reader.Read())
            {
                var currentSupplier = GetSupplierFromReader(reader);
                suppliers.Add(currentSupplier);
            }

            reader.Close();

            return suppliers;
        }

        public Supplier GetSupplierFromReader(SqlDataReader reader)
        {
            var currentSupplier = new Supplier();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var fieldName = reader.GetName(i);

                switch (fieldName)
                {
                    case nameof(Supplier.ID):
                        currentSupplier.ID = (int)reader[i];
                        break;

                    case nameof(Supplier.Name):
                        currentSupplier.Name = (string)reader[i];
                        break;

                    case nameof(Supplier.Phone):
                        currentSupplier.Phone = (string)reader[i];
                        break;

                    case nameof(Supplier.EMail):
                        currentSupplier.EMail = (string)reader[i];
                        break;


                    default:
                        Console.WriteLine($"Field  {fieldName} isn`t in class Customer, or it filling not implemented");
                        break;
                }
            }

            return currentSupplier;
        }

        public Supplier GetSupplier(int id)
        {
            var reader = SQLRepositoryHelper.ExecuteGetObjectByID(id, this);

            if (reader == null)
                return null;

            var supplier = GetSupplierFromReader(reader);
            reader.Close();
            return supplier;
        }

        public void UpdateSupplier(Supplier supplier)
        {
            SQLRepositoryHelper.ExecuteUpdatingObject(supplier, this);
        }
    }
}
